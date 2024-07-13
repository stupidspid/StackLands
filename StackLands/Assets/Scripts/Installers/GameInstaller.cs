using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputService inputService;
    [SerializeField] private CardController card;
    [SerializeField] private BoosterPackController booster;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Camera>().FromInstance(mainCamera);
        Container.BindInterfacesAndSelfTo<InputService>().FromInstance(inputService);
        Container.BindInterfacesTo<CardController>().AsSingle();
        Container.BindFactory<CardController, CardsFactory>().FromComponentInNewPrefab(card);
        Container.BindInterfacesTo<BoosterPackController>().AsSingle();
        Container.BindFactory<BoosterPackController, BoostersPackFactory>().FromComponentInNewPrefab(booster);
        Container.Bind<LayersService>().AsSingle();
        Container.Bind<StackController>().AsSingle();
        Container.Bind<RecipeController>().AsSingle();
    }
}