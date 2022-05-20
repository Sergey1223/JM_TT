using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JM.TestTask
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField]
        private InputService _inputService;

        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private Text _scoreCounterRender;

        private void BindInputService()
        {
            Container
                .Bind<InputService>()
                .FromInstance(_inputService)
                .AsSingle();
        }

        private void BindResetService()
        {
            Container
                .BindInterfacesAndSelfTo<ResetService>()
                .AsSingle();
        }

        private void BindPositionService()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_mainCamera);
           
            Container
                .BindInterfacesAndSelfTo<PositionUpdatingService>()
                .AsSingle();
        }

        private void BindScoreCounterService()
        {
            Container
                .Bind<Text>()
                .FromInstance(_scoreCounterRender);

            Container
                .BindInterfacesAndSelfTo<ScoreCounter>()
                .AsSingle();
        }

        public override void InstallBindings()
        {
            BindInputService();
            BindResetService();
            BindPositionService();
            BindScoreCounterService();
        }
    }
}
