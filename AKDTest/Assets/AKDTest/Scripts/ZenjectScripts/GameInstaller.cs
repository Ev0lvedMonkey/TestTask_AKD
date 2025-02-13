using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private HoldPoint _holdPoint;
    [SerializeField] private ObjectPicker _objectPicker;
    [SerializeField] private TouchInputHandler _touchInputHandler;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPicker>().FromInstance(_objectPicker);
        Container.Bind<FixedJoystick>().FromInstance(_joystick);
        Container.Bind<HoldPoint>().FromInstance(_holdPoint);
        Container.Bind<TouchInputHandler>().FromInstance(_touchInputHandler);
    }
}