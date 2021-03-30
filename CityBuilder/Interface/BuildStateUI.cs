using System;
using System.Collections.Generic;
using System.Text;
using CityBuilder.GameCode;

namespace CityBuilder.Interface
{
    public class BuildStateUI : UIGroup
    {
        private Dictionary<Resource.ResourceType, TextBox> _resources;
        public BuildStateUI()
        {
            _resources = new Dictionary<Resource.ResourceType, TextBox>();
        }

        public bool UpdateResourceCount(Resource.ResourceType type, int value)
        {
            if (_resources.ContainsKey(type))
            {
                if (value > Config.MAX_RESOURCE_VALUE)
                    value = Config.MAX_RESOURCE_VALUE;
                _resources[type].SetText(value.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Register(TextBox textBox, Resource.ResourceType type)
        {
            if (_resources.ContainsKey(type))
                return false;
            this.Add(textBox);
            _resources.Add(type, textBox);
            return true;
        }
    }
}
