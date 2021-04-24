using System;
using System.Collections.Generic;
using System.Text;
using CityBuilder.GameCode;

namespace CityBuilder.Interface
{
    public class BuildStateUI : UIGroup
    {
        private Dictionary<Resource.ResourceType, TextBox> _resourceTextBoxes;
        private Dictionary<Resource.ResourceType, int> resourcesCounts;
        private ScrollBox scrollBox;
        public ScrollBox ScrollBox { get { return scrollBox; } }
        public BuildStateUI()
        {
            _resourceTextBoxes = new Dictionary<Resource.ResourceType, TextBox>();
            resourcesCounts = new Dictionary<Resource.ResourceType, int>();
        }

        public void Initialize(Town town)
        {
            scrollBox.Initialize(town);
        }

        public bool UpdateResourceCount(Resource.ResourceType type, int value)
        {
            if (_resourceTextBoxes.ContainsKey(type))
            {
                if (value > Config.MAX_RESOURCE_VALUE_LABEL)
                    value = Config.MAX_RESOURCE_VALUE_LABEL;
                _resourceTextBoxes[type].SetText(value.ToString());
                if (!resourcesCounts.ContainsKey(type))
                    resourcesCounts.Add(type, value);
                else
                    resourcesCounts[type] = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateResourceCounts(Dictionary<Resource.ResourceType, float> resources)
        {
            bool error = false;
            if (resources == null)
                throw new Exception("Invalid Dictionary, Dictionary is null");
            foreach (Resource.ResourceType type in resources.Keys)
            {
                if (_resourceTextBoxes.ContainsKey(type))
                {
                    int value = (int)resources[type];
                    if (value > Config.MAX_RESOURCE_VALUE_LABEL)
                        value = Config.MAX_RESOURCE_VALUE_LABEL;
                    if (!resourcesCounts.ContainsKey(type))
                    {
                        resourcesCounts.Add(type, value);
                        _resourceTextBoxes[type].Text = value.ToString();
                    }
                    if (resourcesCounts[type] != value)
                    {
                        resourcesCounts[type] = value;
                        _resourceTextBoxes[type].Text = value.ToString();
                    }
                }
                else
                {
                    error = true;
                }
            }
            return !error;
        }

        public bool RegisterResourceLabel(TextBox textBox, Resource.ResourceType type)
        {
            if (_resourceTextBoxes.ContainsKey(type))
                return false;
            this.Add(textBox);
            _resourceTextBoxes.Add(type, textBox);
            return true;
        }

        internal void RegisterScrollBox(ScrollBox scrollBox)
        {
            this.scrollBox = scrollBox;
        }
    }
}
