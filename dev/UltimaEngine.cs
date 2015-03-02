/***************************************************************************
 *   Engine.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.UltimaLogin;
using UltimaXNA.UltimaWorld;
#endregion

namespace UltimaXNA
{
    public class UltimaEngine : Core.BaseEngine
    {
        private static UltimaClient s_Client = new UltimaClient();

        private static Core.AUltimaModel m_Model;
        internal static Core.AUltimaModel ActiveModel
        {
            get { return m_Model; }
            set
            {
                if (m_Model != null)
                {
                    m_Model.Dispose();
                    m_Model = null;
                }
                m_Model = value;
                m_Model.Initialize(s_Client);
            }
        }

        public UltimaEngine(int width, int height)
            :base(width, height)
        {
            UltimaVars.EngineVars.ScreenSize = new Point2D(width, height);
        }

        protected override void OnInitialize()
        {
            IsometricRenderer.Initialize(this);

            // Make sure we have a UO installation before loading UltimaData.
            if (UltimaData.FileManager.IsUODataPresent)
            {
                // Initialize and load data
                UltimaData.AnimationData.Initialize(GraphicsDevice);
                UltimaData.ArtData.Initialize(GraphicsDevice);
                UltimaData.Fonts.ASCIIText.Initialize(GraphicsDevice);
                UltimaData.Fonts.UniText.Initialize(GraphicsDevice);
                UltimaData.GumpData.Initialize(GraphicsDevice);
                UltimaData.HuesXNA.Initialize(GraphicsDevice);
                UltimaData.TexmapData.Initialize(GraphicsDevice);
                UltimaData.StringData.LoadStringList("enu");
                UltimaData.SkillsData.Initialize();
                GraphicsDevice.Textures[1] = UltimaXNA.UltimaData.HuesXNA.HueTexture;

                UltimaVars.EngineVars.EngineRunning = true;
                UltimaVars.EngineVars.InWorld = false;
                UltimaInteraction.Initialize(s_Client);

                ActiveModel = new LoginModel();
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            this.IsFixedTimeStep = UltimaVars.EngineVars.LimitFPS;
            if (!UltimaVars.EngineVars.EngineRunning)
            {
                Exit();
            }
            else
            {
                UltimaInteraction.Update();
                s_Client.Update();
                UltimaVars.EngineVars.GameTime = gameTime;
                ActiveModel.Update(gameTime.TotalGameTime.TotalMilliseconds, gameTime.ElapsedGameTime.TotalMilliseconds);
            }
        }

        protected override void OnDraw(GameTime gameTime)
        {
            if (UltimaVars.EngineVars.InWorld)
                IsometricRenderer.Draw(gameTime);
            UltimaInteraction.Draw();
        }
    }
}