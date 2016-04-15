
using System.ComponentModel;

namespace PhotoBoothPlugInSDK
{
    public class Parameter
    {
        private string _name;
        private dynamic _value;
        private dynamic _min;
        private dynamic _max;
        private dynamic _default;
        private int _tick;
        private parameterDisplayType _displayType;
        private bool _isSet;

        public Parameter(string Name, int Tick, dynamic Min, dynamic Max, dynamic Default, parameterDisplayType dType)
        {
            if (Min.GetType() == Max.GetType() && Min.GetType() == Default.GetType())
            {
                _name = Name;
                _tick = Tick;
                _min = Min;
                _max = Max;
                _default = Default;
                _displayType = dType;
                _isSet = false;
            }
            else
            {
                throw new DifferentTypesException();
            }
        }

        public void setValue(dynamic value)
        {
            if (value.GetType() == _default.GetType())
            {
                _value = value;
                _isSet = true;
            }
            else
            {
                throw new DifferentTypesException();
            }
        }

        public int getTick()
        {
            return _tick;
        }

        public dynamic getValue()
        {
            if (_isSet && _value != null)
                return _value;
            else
            {
                _isSet = false;
                return _default;
            }
        }

        public dynamic getMin()
        {
            return _min;
        }

        public dynamic getMax()
        {
            return _max;
        }

        public dynamic getDefault()
        {
            return _default;
        }

        public parameterDisplayType getDisplayType()
        {
            return _displayType;
        }

        public string getName()
        {
            return _name;
        }
    }

    public enum parameterDisplayType
    {
        number,
        range,
        text,
        list
    }
}
