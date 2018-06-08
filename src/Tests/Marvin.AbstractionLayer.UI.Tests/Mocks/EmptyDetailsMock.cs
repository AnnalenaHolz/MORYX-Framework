﻿namespace Marvin.AbstractionLayer.UI.Tests
{
    [DetailsComponentRegistration(DetailsConstants.EmptyType)]
    public class EmptyDetailsMock : EmptyDetailsViewModelBase, IDetailsComponent
    {
        public bool InitializeCalled { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeCalled = true;
        }
    }
}