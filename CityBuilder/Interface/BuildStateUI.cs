using System;
using System.Collections.Generic;
using System.Text;
using CityBuilder.GameCode;

namespace CityBuilder.Interface
{
    public class BuildStateUI : UIGroup
    {
        private Dictionary<Resource.ResourceType, TextBox> _resources;
        private ScrollBox scrollBox;
        public ScrollBox ScrollBox { get { return scrollBox; } }
        public BuildStateUI()
        {
            _resources = new Dictionary<Resource.ResourceType, TextBox>();
        }

        public void Initialize(Town town)
        {
            scrollBox.Initialize(town);
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

        public bool RegisterResourceLabel(TextBox textBox, Resource.ResourceType type)
        {
            if (_resources.ContainsKey(type))
                return false;
            this.Add(textBox);
            _resources.Add(type, textBox);
            return true;
        }

        internal void RegisterScrollBox(ScrollBox scrollBox)
        {
            this.scrollBox = scrollBox;
        }
    }
}
