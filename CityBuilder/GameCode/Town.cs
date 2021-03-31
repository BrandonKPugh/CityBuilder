﻿using CityBuilder.GameCode;
using CityBuilder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Town
    {
        private SpriteSheet _spriteSheet;
        private List<Structure> _structures;
        private Dictionary<Resource.ResourceType, int> _resources;

        private bool _contentLoaded = false;

        public Town()
        {
            _structures = new List<Structure>();
            _resources = new Dictionary<Resource.ResourceType, int>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Structure structure in _structures)
            {
                structure.Draw(spriteBatch);
            }
        }

        public void Initialize()
        {
            resetResources();
        }

        private void resetResources()
        {
            foreach(Resource.ResourceType type in Enum.GetValues(typeof(Resource.ResourceType)))
            {
                switch (type)
                {
                    case Resource.ResourceType.Wood:
                        {
                            _resources.Add(type, Config.INITIAL_RESOURCE_VALUE_WOOD);
                            break;
                        }
                    case Resource.ResourceType.Stone:
                        {
                            _resources.Add(type, Config.INITIAL_RESOURCE_VALUE_STONE);
                            break;
                        }
                    case Resource.ResourceType.Ore:
                        {
                            _resources.Add(type, Config.INITIAL_RESOURCE_VALUE_ORE);
                            break;
                        }
                    case Resource.ResourceType.Metal:
                        {
                            _resources.Add(type, Config.INITIAL_RESOURCE_VALUE_METAL);
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("Resource Type: " + type.ToString() + " does not have an initial value!");
                        }
                }
            }
        }

        public void LoadContent(SpriteSheet spriteSheet)
        {
            _spriteSheet = spriteSheet;
            foreach(Structure structure in _structures)
            {
                LoadStructureContent(structure);
            }
            _contentLoaded = true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Structure structure in _structures)
            {
                structure.Update(gameTime);
            }
        }

        public void AddStructure(Structure structure)
        {
            _structures.Add(structure);
            if(_contentLoaded)
            {
                LoadStructureContent(structure);
            }
        }

        public void LoadStructureContent(Structure structure)
        {
            structure.LoadContent(_spriteSheet.GetSprite("structure-" + structure.Data.Width + 'x' + structure.Data.Height));
        }

        public bool CanCreateStructure(Structure.StructureType structureType)
        {
            Dictionary<Resource.ResourceType, int> cost = Structure.GetStructureCost(structureType);
            foreach(Resource.ResourceType type in cost.Keys)
            {
                if (_resources[type] < cost[type])
                    return false;
            }
            return true;
        }

        public void AddCards(ScrollBox scrollBox)
        {
            foreach (Structure.StructureType type in Enum.GetValues(typeof(Structure.StructureType)))
            {
                switch (type)
                {
                    case Structure.StructureType.House:
                        {
                            scrollBox.AddCard(type, null);
                            break;
                        }
                    case Structure.StructureType.Warehouse:
                        {
                            scrollBox.AddCard(type, null);
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("Structure Type: " + type.ToString() + " not defined!");
                        }
                }
            }
        }

        private EventHandler BeginStructurePlacement(Structure.StructureType type)
        {
            throw new NotImplementedException();
        }
    }
}
