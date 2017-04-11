using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace ViewManagement
{
    public class Parameter : Dictionary<string, object>
    {
        public Parameter()
        {
        }

        public Parameter(string name, object value)
        {
            this[name] = value;
        }

        public Parameter(Dictionary<string, object> parameters)
        {
            foreach (var parameter in parameters)
            {
                this[parameter.Key] = parameter.Value;
            }
        }

        public object GetParameter(string parameterName)
        {
            return this[parameterName];
        }

        public T GetParameter<T>(string parameterName)
        {
            var parameter = this[parameterName];
            if (parameter == null)
                return default(T);
            else
                return (T)parameter;
        }

        public void SetParameter(string parameterName, object parameterValue)
        {
            this[parameterName] = parameterValue;
        }
    }

    public class ViewModelBaseWrapper : ViewModelBase
    {
        public Parameter Parameters;

        public virtual void NavigatedFrom()
        {
        }

        public virtual void NavigatedTo()
        {
        }
    }
}