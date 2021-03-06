using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab6_7.ViewModels.Base
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Обновление значения свойства для поля (для обхода ивентлупа)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">Ссылка на поле</param>
        /// <param name="value">Новое значение</param>
        /// <param name="PropertyName">Передается в OnPropertyChanged</param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
