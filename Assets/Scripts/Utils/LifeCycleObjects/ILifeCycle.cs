namespace JCC.Utils.LifeCycle
{
    interface ILifeCycle
    {
        public bool Initialization(params object[] parameters);
        public bool WasInitialized();
    }
}

