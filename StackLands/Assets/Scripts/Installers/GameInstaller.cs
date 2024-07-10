using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputService inputService;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Camera>().FromInstance(mainCamera);
        Container.BindInterfacesAndSelfTo<InputService>().FromInstance(inputService);
    }
}