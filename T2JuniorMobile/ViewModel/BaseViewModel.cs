using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// Базовый класс ViewModel, реализующий интерфейс INotifyPropertyChanged.
/// </summary>

public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Событие, возникающее при изменении свойства.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Метод для уведомления об изменении свойства.
    /// </summary>
    /// <param name="propertyName">Имя измененного свойства.</param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Метод для установки значения свойства с уведомлением.
    /// </summary>
    /// <typeparam name="T">Тип свойства.</typeparam>
    /// <param name="backingStore">Ссылка на хранилище значения свойства.</param>
    /// <param name="value">Новое значение свойства.</param>
    /// <param name="propertyName">Имя свойства.</param>
    /// <returns>True, если значение изменилось; иначе, false.</returns>
    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}