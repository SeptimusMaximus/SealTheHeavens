using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SealTheHeavens
{
    public static class Helper
    {
        public static ModPlayer Revenant(this Player player)
        {
            return player.GetModPlayer<STHPlayer>();
        }
        public static void Kill(this NPC npc)
        {
            NPCLoader.PreNPCLoot(npc);
            NPCLoader.NPCLoot(npc);
            npc.HitEffect();
            npc.life = 1;
            npc.active = false;
        }
        public static class Projectiles
        {
            public static void ProjCircle(Vector2 pos, int amount, int type, float speed, int damage)
            {
                float rot = (float)(Math.PI * 2) / amount;
                for (int i = 0; i <= amount; i++)
                {
                    Projectile.NewProjectile(pos, new Vector2(speed, 0).RotatedBy(rot * i), type, damage, 0f);
                }
            }
        }
        public static class Drawing
        {
            /// <summary>
            /// Draws a texture from point to point, cutting it when needed. <br/>
            /// <summary>
            public static void DrawChain(Texture2D texture, Vector2 from, Vector2 to, Color color2)
            {
                Rectangle? sourceRectangle = new Rectangle?();
                Vector2 origin = new(texture.Width * 0.5f, texture.Height * 0.5f);
                float height = texture.Height;
                Vector2 vector1 = from - to;
                float rotation = (float)Math.Atan2(vector1.Y, vector1.X) - 1.57f;
                bool draw = true;
                if (float.IsNaN(to.X) && float.IsNaN(to.Y))
                    draw = false;
                if (float.IsNaN(vector1.X) && float.IsNaN(vector1.Y))
                    draw = false;
                while (draw)
                    if (vector1.Length() < height + 1.0)
                    {
                        draw = false;
                    }
                    else
                    {
                        Vector2 vector2 = vector1;
                        vector2.Normalize();
                        to += vector2 * height;
                        vector1 = from - to;
                        Main.spriteBatch.Draw(texture, to - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    }
            }
            /// <summary>
            /// Draws a tooltip, used to apply shaders to text, uses dye ids. <br/>
            /// <summary>
            public static void DrawTooltipLine(DrawableTooltipLine line, Color color1, Color color2, int dyeID = -1)
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                if (dyeID != -1)
                {
                    ArmorShaderData shader = GameShaders.Armor.GetShaderFromItemId(dyeID);
                    shader.UseColor(color1);
                    shader.UseSecondaryColor(color2);
                    shader.Apply(null);
                }
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            }
            /// <summary>
            /// Draws a tooltip, used to apply shaders to text, directly uses a shader. <br/>
            /// <summary>
            public static void DrawTooltipLine2(DrawableTooltipLine line, Color color1, Color color2, ArmorShaderData shader = null)
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                if (shader != null)
                {
                    shader.UseColor(color1);
                    shader.UseSecondaryColor(color2);
                    shader.Apply(null);;
                }
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            }
            /// <summary>
            /// Draws a tooltip, used to apply shaders to text, uses passnames to find modded shaders. <br/>
            /// <summary>
            public static void DrawTooltipLine3(DrawableTooltipLine line, Color color1, Color color2, string passName = null)
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
                if (passName != null)
                {
                    var shader = GameShaders.Misc[passName];
                    if (shader != null)
                    {
                        shader.UseColor(color1);
                        shader.UseSecondaryColor(color2);
                        shader.Apply(null);
                    }
                }
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            }
        }
        public static class NPCs
        {
            /// <summary>
            /// Finds the nearest NPC. <br/>
            /// <summary>
            public static int FindNearestNPC(Vector2 position, float maxDistance = 999999f)
            {
                int nearestTarget = -1;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];
                    float distance = Vector2.Distance(n.Center, position);
                    if (distance <= maxDistance && (nearestTarget == -1 || Vector2.Distance(Main.npc[nearestTarget].Center, position) > distance) && Main.npc[i].CanBeChasedBy())
                        nearestTarget = i;
                }
                return nearestTarget;
            }
            /// <summary>
            /// Finds the nearest NPC but is direct. <br/>
            /// <summary>
            public static NPC FindNearestNPCDirect(Vector2 position, float maxDistance = 9999999f)
            {
                int nearestTarget = -1;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];
                    float distance = Vector2.Distance(n.Center, position);
                    if (distance <= maxDistance && (nearestTarget == -1 || Vector2.Distance(Main.npc[nearestTarget].Center, position) > distance) && Main.npc[i].CanBeChasedBy())
                        nearestTarget = i;
                }
                return (nearestTarget != -1 && nearestTarget <= 200) ? Main.npc[nearestTarget] : null;
            }
        }
        public static class Players
        {
            /// <summary>
            /// Finds the lowest player. <br/>
            /// <summary>
            public static int FindLowestPlayer(Vector2 position, float maxDistance)
            {
                int lowestPlayer = -1;
                for (int i = 0; i < 255; i++)
                {
                    Player target = Main.player[i];
                    if (target.active && (lowestPlayer == -1 || target.statLife < Main.player[lowestPlayer].statLife))
                        lowestPlayer = i;
                }
                return lowestPlayer;
            }
            /// <summary>
            /// Finds the lowest player but is direct. <br/>
            /// <summary>
            public static Player FindLowestPlayerDirect(Vector2 position, float maxDistance)
            {
                int lowestPlayer = -1;
                for (int i = 0; i < 255; i++)
                {
                    Player target = Main.player[i];
                    if (target.active && (lowestPlayer == -1 || target.statLife < Main.player[lowestPlayer].statLife))
                        lowestPlayer = i;
                }
                return lowestPlayer != -1 ? Main.player[lowestPlayer] : null;
            }
            /// <summary>
            /// Increases the player's crit by the amount. <br/>
            /// <summary>
            public static void AllCritUp(Player player, int crit)
            {
                player.meleeCrit += crit;
                player.magicCrit += crit;
                player.rangedCrit += crit;
            }
            /// <summary>
            /// Increases the player's damage by the amount. <br/>
            /// <summary>
            public static void AllDamageUp(Player player, float damage)
            {
                player.meleeDamage += damage;
                player.magicDamage += damage;
                player.rangedDamage += damage;
                player.minionDamage += damage;
            }
        }
        public static class Dusts
        {
            /// <summary>
            /// Spawns a dust circle at a position. <br/>
            /// <summary>
            public static void DustCircle(Vector2 pos, int amount, int dust, float speed, float scale)
            {
                for (int i = 0; i < amount; i++)
                {
                    Vector2 vector1 = Vector2.UnitY * speed;
                    vector1 = vector1.RotatedBy((i - (amount / 2 - 1)) * 6.28318548f / amount) + pos;
                    Vector2 vector2 = vector1 - pos;
                    int d = Dust.NewDust(vector1 + vector2, 0, 0, dust, 0f, 0f, 0, default, scale);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity = vector2;
                }
            }
            /// <summary>
            /// Spawns a dust aura in a position. <br/>
            /// <summary>
            public static void DustAura(Vector2 pos, int type, int amount, float distance, float scale = 1f, float difference = 0f)
            {
                for (int i = 0; i < 20; i++)
                {
                    Vector2 offset = new();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI; // Main.rand.NextDouble * 2d * Math.PI
                    offset.X += (float)(Math.Sin(angle) * (distance + Main.rand.NextFloat(-difference / 2, difference / 2)));
                    offset.Y += (float)(Math.Cos(angle) * (distance + Main.rand.NextFloat(-difference / 2, difference / 2)));
                    Dust dust = Main.dust[Dust.NewDust(pos + offset - new Vector2(4, 4), 0, 0, type, 0, 0, 100)];
                    //if (Main.rand.Next(3) == 0)
                        //dust.velocity += Vector2.Normalize(offset) * -30f;
                    dust.noGravity = true;
                }
            }
            /// <summary>
            /// Spawns a dust line from position to position. <br/>
            /// <summary>
            public static void DustLine(Vector2 from, Vector2 to, int amount, int dust, float scale)
            {
                Vector2 offset = to - from;
                offset.Normalize();
                offset *= Vector2.Distance(from, to) / amount;
                for (int i = 0; i <= amount; i++)
                {
                    int d = Dust.NewDust(from + offset * i, 0, 0, dust, 0f, 0f, 0, default, scale);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity = Vector2.Zero;
                }
            }
            public static void DustLightning(Vector2 from, Vector2 to, int dust, float scale, int rad)
            {
                Vector2 offset = to - from;
                Vector2[] pos = new Vector2[16];
                offset.Normalize();
                offset *= Vector2.Distance(from, to) / 16;
                for (int i = 0; i <= 16; i++)
                {
                    pos[i] = from + offset.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-rad, rad + 1))) * i;
                }
                for (int i = 0; i <= 15; i++)
                {
                    DustLine(pos[i], pos[i+1], 16, dust, 1f);
                }
            }
			public static void DustCircle2(Vector2 pos, int type, int amount, float rotation, Color color)
			{
				for (int i = 0; (float)i < amount; i++)
				{
					Vector2 spinPoint = Vector2.UnitX * 0f;
					spinPoint += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / amount)) * new Vector2(1f, 4f);
                    spinPoint = spinPoint.RotatedBy(rotation);
					int num6 = Dust.NewDust(pos, 0, 0, type, 0, 0, 0, color);
					Main.dust[num6].scale = 1.5f;
					Main.dust[num6].noGravity = true;
					Main.dust[num6].position = pos + spinPoint;
					Main.dust[num6].velocity = spinPoint.SafeNormalize(Vector2.UnitY);
				}
			}
        }
        public static class Syncing
        {
            /// <summary>
            /// Synchronizes a Projectile. <br/>
            /// <summary>
            public static void SyncProjectile(int Projectile)
            {
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile);
            }
            /// <summary>
            /// Synchronizes a NPC. <br/>
            /// <summary>
            public static void SyncNPC(int npc)
            {
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc);
            }
        }
        public static class Text
        {
            /// <summary>
            /// Colors text in the terraria way. <br/>
            /// <summary>
            public static string ColorText(string text, Color color)
            {
                return "[c/" + color.Hex3() + ":" + text + "]";
            }
            /// <summary>
            /// Transforms item ids into text. <br/>
            /// <summary>
            public static string ItemText(int id)
            {
                return  "[i:" + id + "]";
            }
            /// <summary>
            /// Transforms button ids into text. <br/>
            /// <summary>
            public static string ButtonText(int buttonid)
            {
                return "[g:" + buttonid + "]";
            }
            public static string GlitchText(string text, float chance)
            {
                string text2 = "";
                for (int i = 0; i < text.Length; i++)
                {
                    char character = text[i];
				    if (Main.rand.NextFloat(0, 1) <= chance)
				    {
					    character = (char)(33 + Main.rand.Next(15));
				    }
				text2 += character;
                }
                return text2;
            }
            public static string GlitchText2(string text, float chance)
            {
                string text2 = "";
                for (int i = 0; i < text.Length; i++)
                {
                    char character = text[i];
				    if (Main.rand.NextFloat(0, 1) <= chance)
				    {
					    character = (char)(33 + Main.rand.Next(15));
				    }
				text2 += character;
                }
                return ColorTextRandom(text2);
            }
            public static string ColorTextRandom(string text)
            {
                string text2 = "";
                for (int i = 0; i < text.Length; i++)
                {
                    text2 += Helper.Text.ColorText(text[i].ToString(), Helper.Colors.Random());
                }
                return text2;
            }
        }
        public static class Numbers
        {
            /// <summary>
            /// Cycles a number from 0 to 1 to 0. <br/>
            /// Mode 1 = -1 to 1.
            /// Mode 2 = 0 to 1.
            /// <summary>
            public static float ZerotoOne(float multiplier = 1f, int mode = 0, float differentiator = 0)
            {
                double rad = mode == 0 ? Math.PI : mode == 1 ? Math.PI * 2 : Math.PI / 2;
                return (float)Math.Sin((Main.GlobalTimeWrappedHourly + differentiator) * multiplier % rad);
            }
        }
        public static class Colors
        {
            /// <summary>
            /// Celestial color used for celestial items. <br/>
            /// <summary>
            public static Color Celestial(float speed = 1f)
            {
                return Helper.Colors.Lerp4(Color.Orange, Color.LightBlue, Color.Violet, Color.Cyan, Helper.Numbers.ZerotoOne(1, 2));
            }
            public static Color Random()
            {
                return new Color(Main.rand.Next(0, 256), Main.rand.Next(0, 256), Main.rand.Next(0, 256));
            }
            public static Color Lerp3(Color color1, Color color2, Color color3, float amount)
            {
                Color color = Color.Transparent;
                if (amount <= .33f)
                {
                    color = Color.Lerp(color1, color2, amount * 3);
                }
                else if (amount > .33f && amount <= .66f)
                {
                    color = Color.Lerp(color2, color3, (amount - .33f) * 3);
                }
                else if (amount > .66f && amount <= 1f)
                {
                    color = Color.Lerp(color3, color1, (amount - .66f) * 3);
                }
                return color;
            }
            public static Color Lerp4(Color color1, Color color2, Color color3, Color color4, float amount)
            {
                Color color = Color.Transparent;
                if (amount <= .25f)
                {
                    color = Color.Lerp(color1, color2, amount * 4);
                }
                else if (amount > .25f && amount <= .5f)
                {
                    color = Color.Lerp(color2, color3, (amount - .25f) * 4);
                }
                else if (amount > .5f && amount <= .75f)
                {
                    color = Color.Lerp(color3, color4, (amount - .5f) * 4);
                }
                else if (amount > .75f && amount < 1f)
                {
                    color = Color.Lerp(color4, color1, (amount - .75f) * 4);
                }
                return color;
            }
            public static Color LerpPlus(Color[] colors, float amount)
            {
                Color color = Color.Transparent;
                for (int i = 0; i <= colors.Length; i++)
                {
                    if (amount > 1f / colors.Length * i && amount < 1f / colors.Length * (i + 1))
                    {
                        if (i != colors.Length)
                        color = Color.Lerp(colors[i], colors[i + 1], (amount - (amount / colors.Length) * i) * colors.Length);
                        else
                        {
                            color = Color.Lerp(colors[colors.Length], colors[0], (amount - (amount / colors.Length) * i) * colors.Length);
                        }
                    }
                }
                return color;
            }
        }
    }
}
