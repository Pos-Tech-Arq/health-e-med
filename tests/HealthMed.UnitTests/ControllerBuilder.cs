
namespace HealthMed.UnitTests;

public class ControllerBuilder<C,S> where C : class where S : class
{
    protected Mock<C> _controller;
    protected Mock<S> _service;

    public ControllerBuilder()
    {
        _service = new Mock<S>();
        _controller = new Mock<C>(_service.Object);
    }
}
