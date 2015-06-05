﻿using UltimaXNA.Core.Diagnostics.Tracing;
using UltimaXNA.Core.UI;

namespace UltimaXNA.Ultima.Login.States
{
    public class StateManager
    {
        AState m_CurrentScene;
        bool m_isTransitioning = false;

        public bool IsTransitioning
        {
            get { return m_isTransitioning; }
        }

        public AState CurrentState
        {
            get { return m_CurrentScene; }
            set
            {
                if (m_isTransitioning)
                    return;

                m_isTransitioning = true;

                if (m_CurrentScene != null)
                {
                    Tracer.Debug("Starting scene transition from {0} to {1}", m_CurrentScene.GetType().Name, value == null ? "Null" : value.GetType().Name);
                    m_CurrentScene.SceneState = SceneState.TransitioningOff;

                    if (value == null)
                    {
                        m_CurrentScene.Dispose();
                        m_CurrentScene = null;
                    }
                    else
                    {
                        m_CurrentScene.TransitionCompleted += new TransitionCompleteHandler(delegate()
                        {
                            Tracer.Debug("Scene transition complete.");
                            Tracer.Debug("Disposing {0}.", m_CurrentScene.GetType().Name);

                            m_CurrentScene.Dispose();
                            m_CurrentScene = value;
                            if (m_CurrentScene != null)
                            {
                                m_CurrentScene.Manager = this;

                                if (!m_CurrentScene.IsInitialized)
                                {
                                    Tracer.Debug("Initializing {0}.", m_CurrentScene.GetType().Name);
                                    m_CurrentScene.Intitialize();
                                }
                            }

                            m_isTransitioning = false;
                        });
                    }
                }
                else
                {
                    Tracer.Debug("Starting scene {0}", value.GetType().Name);
                    m_CurrentScene = value;
                    m_CurrentScene.Manager = this;

                    if (!m_CurrentScene.IsInitialized)
                    {
                        Tracer.Debug("Initializing {0}.", m_CurrentScene.GetType().Name);
                        m_CurrentScene.Intitialize();
                    }

                    m_isTransitioning = false;
                }
            }
        }

        UserInterfaceService m_UserInterface;
        LoginModel m_Login;

        public StateManager()
        {
            m_UserInterface = ServiceRegistry.GetService<UserInterfaceService>();
            m_Login = ServiceRegistry.GetService<LoginModel>();
        }

        public void Update(double totalTime, double frameTime)
        {
            AState current = m_CurrentScene;

            if (m_CurrentScene != null)
                m_CurrentScene.Update(totalTime, frameTime);

            //This is just incase a scene changes in the middle of updating.
            if (current != m_CurrentScene && m_CurrentScene != null)
            {
                m_CurrentScene.Update(totalTime, frameTime);
            }
        }

        public void ResetToLoginScreen()
        {
            m_Login.Client.Disconnect();
            m_UserInterface.Reset();
            if (!(m_CurrentScene is LoginState))
                CurrentState = new LoginState();
        }
    }
}
