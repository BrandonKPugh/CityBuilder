using CityBuilder.GameCode;
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
        public delegate bool SetResourceLabelMethod(Dictionary<Resource.ResourceType, float> resources);

        private SpriteSheet _spriteSheet;
        private List<Structure> _structures;
        private Dictionary<Resource.ResourceType, float> _resources;
        private Grid _grid;
        private GhostStructure _ghostStructure;

        public SetResourceLabelMethod SetResourceLabel;

        private bool _contentLoaded = false;

        public Town(Grid grid)
        {
            _structures = new List<Structure>();
            _resources = new Dictionary<Resource.ResourceType, float>();
            this._grid = grid;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Structure structure in _structures)
            {
                structure.Draw(spriteBatch);
            }
            if(_ghostStructure != null)
            {
                _ghostStructure.Draw(spriteBatch);
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
            Calculate();

            foreach (Structure structure in _structures)
            {
                structure.Update(gameTime);
            }
            if (_ghostStructure != null)
                _ghostStructure.Update(gameTime);
            
            if(SetResourceLabel == null)
            {
                throw new Exception("SetResourceLabelMethod not set!");
            }
            else
            {
                SetResourceLabel(_resources);
            }
        }

        private void Calculate()
        {
            // Get maximum resource count
            Dictionary<Resource.ResourceType, int> resourcesMax;
            resourcesMax = new Dictionary<Resource.ResourceType, int>();
            resourcesMax.Add(Resource.ResourceType.Wood, Config.CAPITAL_RESOURCE_ADD_WOOD);
            resourcesMax.Add(Resource.ResourceType.Stone, Config.CAPITAL_RESOURCE_ADD_STONE);
            resourcesMax.Add(Resource.ResourceType.Ore, Config.CAPITAL_RESOURCE_ADD_ORE);
            resourcesMax.Add(Resource.ResourceType.Metal, Config.CAPITAL_RESOURCE_ADD_METAL);

            // Add/Subtract all resources
            foreach (Structure structure in _structures)
            {
                switch (structure.Data.Type)
                {
                    case Structure.StructureType.House:
                        {
                            _resources[Resource.ResourceType.Wood] += 0.01f;
                            _resources[Resource.ResourceType.Stone] += 0.01f;
                            _resources[Resource.ResourceType.Ore] += 0.01f;
                            _resources[Resource.ResourceType.Metal] += 0.01f;
                            break;
                        }
                    case Structure.StructureType.Warehouse:
                        {
                            resourcesMax[Resource.ResourceType.Wood] += Config.WAREHOUSE_RESOURCE_ADD_WOOD;
                            resourcesMax[Resource.ResourceType.Stone] += Config.WAREHOUSE_RESOURCE_ADD_STONE;
                            resourcesMax[Resource.ResourceType.Ore] += Config.WAREHOUSE_RESOURCE_ADD_ORE;
                            resourcesMax[Resource.ResourceType.Metal] += Config.WAREHOUSE_RESOURCE_ADD_METAL;
                            break;
                        }
                    case Structure.StructureType.Lumbermill:
                        {
                            _resources[Resource.ResourceType.Wood] += 0.15f;
                            break;
                        }
                    case Structure.StructureType.Capital:
                        {
                            break;
                        }
                    case Structure.StructureType.Mine:
                        {
                            _resources[Resource.ResourceType.Stone] += 0.25f;
                            _resources[Resource.ResourceType.Ore] += 0.15f;
                            break;
                        }
                    case Structure.StructureType.Forge:
                        {
                            _resources[Resource.ResourceType.Ore] -= 0.1f;
                            _resources[Resource.ResourceType.Metal] += 0.05f;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            foreach(Resource.ResourceType type in Enum.GetValues(typeof(Resource.ResourceType)))
            {
                if(_resources[type] > resourcesMax[type])
                {
                    _resources[type] = resourcesMax[type];
                }
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

        public void LoadStructureContent(Structure structure, bool isGhost = false)
        {
            Sprite newSprite = _spriteSheet.GetSprite("structure-" + structure.Data.Width + 'x' + structure.Data.Height);
            if(isGhost)
            {
                Sprite ghostCopy = newSprite.Copy();
                ghostCopy.TextureColor = ControlConstants.GHOST_STRUCTURE_COLOR;
                newSprite = ghostCopy;
            }
            structure.LoadContent(newSprite);
        }

        public bool CanCreateStructure(Structure structure)
        {
            Dictionary<Resource.ResourceType, int> cost = Structure.GetStructureCost(structure);
            foreach(Resource.ResourceType type in cost.Keys)
            {
                if (_resources[type] < cost[type])
                    return false;
            }
            return true;
        }

        public bool CanCreateStructureByType(Structure.StructureType structureType)
        {
            Dictionary<Resource.ResourceType, int> cost = Structure.GetStructureCostByType(structureType);
            foreach (Resource.ResourceType type in cost.Keys)
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
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Warehouse:
                        {
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Lumbermill:
                        {
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Capital:
                        {
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Forge:
                        {
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Mine:
                        {
                            scrollBox.AddCard(type);
                            break;
                        }
                    case Structure.StructureType.Other:
                        {
                            // Don't add a card for Other
                            //scrollBox.AddCard(type);
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException("Structure Type: " + type.ToString() + " not defined!");
                        }
                }
            }
        }

        public void BeginStructurePlacement(Structure.StructureType type)
        {
            _ghostStructure = new GhostStructure(_grid, new Structure.StructureData(Structure.GetStructureDefaultSize(type), -1, -1, type), this);
            LoadStructureContent(_ghostStructure, true);
        }

        public bool FinalizeStructurePlacement(Structure structure, int tileX, int tileY)
        {
            if (ValidStructurePlacementLocation(structure, tileX, tileY))
            {
                TakeStructureCost(structure);
                Structure newPlacedStructure = _ghostStructure.ToStructure();
                LoadStructureContent(newPlacedStructure, false);
                _structures.Add(newPlacedStructure);
                _ghostStructure = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TakeStructureCost(Structure structure)
        {
            Dictionary<Resource.ResourceType, int> cost = structure.Data.Cost;
            if (cost == null)
                cost = Structure.GetStructureCostByType(structure.Data.Type);
            foreach (Resource.ResourceType resourceType in cost.Keys)
            {
                try
                {
                    _resources[resourceType] -= cost[resourceType];
                }
                catch
                {
                    throw new Exception("Invalid resource type in Structure cost. Town does not contain reference to resource: " + resourceType.ToString());
                }
            }
        }

        private bool ValidStructurePlacementLocation(Structure structure, int tileX, int tileY)
        {
            if (!CanCreateStructure(structure))
                return false;
            Structure.StructureSize size = Structure.GetStructureSize(structure);
            for(int y = tileY; y < tileY + size.Height; y++)
            {
                for(int x = tileX; x < tileX + size.Width; x++)
                {
                    if (IsStructureUnderTile(x, y))
                    {
                        return false;
                    }
                    else
                    {

                    }
                }
            }
            // We now know that the structure doesn't overlap any previous structures, but still need to check the bounds of the grid.
            if (tileX >= 0 && tileX + size.Width <= _grid.Info.TilesWide && tileY >= 0 && tileY + size.Height <= _grid.Info.TilesHigh)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsStructureUnderTile(int tileX, int tileY)
        {
            if (GetStructureUnderTile(tileX, tileY) == null)
                return false;
            else
                return true;
        }

        private Structure GetStructureUnderTile(int tileX, int tileY)
        {
            foreach (Structure structure in _structures)
            {
                if (structure.Data.X1 <= tileX && structure.Data.X2 >= tileX && structure.Data.Y1 <= tileY && structure.Data.Y2 >= tileY)
                {
                    return structure;
                }
            }
            return null;
        }

        public void SetResourceCount(Resource.ResourceType type, int count)
        {
            if (_resources.ContainsKey(type))
            {
                if (_resources[type] + count > Config.MAX_RESOURCE_VALUE)
                    _resources[type] = Config.MAX_RESOURCE_VALUE;
                else
                    _resources[type] = count;
            }
            else
            {
                throw new NotImplementedException("That resource type does not exist");
            }
        }

        public void AddResourceToCount(Resource.ResourceType type, int count)
        {
            if (_resources.ContainsKey(type))
            {
                if (_resources[type] + count > Config.MAX_RESOURCE_VALUE)
                    _resources[type] = Config.MAX_RESOURCE_VALUE;
                else
                    _resources[type] = count;
            }
            else
            {
                throw new NotImplementedException("That resource type does not exist");
            }
        }

        public  void CancelPlacementButton_Click(object sender, EventArgs e)
        {
            _ghostStructure = null;
        }
    }
}
