




using System;

namespace Unowhy_Tools_WPF.Services.Contracts;

public interface ITestWindowService
{
    public void Show(Type windowType);

    public T Show<T>() where T : class;
}

