using CityBuilder.GameCode;
using CityBuilder.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Town
    {
        public delegate bool SetResourceLabelMethod(Dictionary<Resource.ResourceType, float> resources);

        private SpriteSheet _spriteSheet;
        //private List<Structure> _structures;
        private Dictionary<int, Structure> _structures;
        private Dictionary<Resource.ResourceType, float> _resources;
        private Grid _grid;
        private GhostStructure _ghostStructure;
        private Structure _selectedStructure;
        private Sprite _selectedStructureOverlay;
        private RectangleBody _townZone;
        private MouseState _lastMouseState;

        public SetResourceLabelMethod SetResourceLabel;

        private bool _contentLoaded = false;

        public Town(Grid grid)
        {
            //_structures = new List<Structure>();
            _structures = new Dictionary<int, Structure>();
            _resources = new Dictionary<Resource.ResourceType, float>();
            this._grid = grid;
            _townZone = new RectangleBody(grid.Info.GridRectangle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Structure structure in _structures.Values)
            {
                if (structure != null)
                {
                    structure.Draw(spriteBatch);
                }
            }
            if (_ghostStructure != null)
            {
                _ghostStructure.Draw(spriteBatch);
            }
            if(_selectedStructure != null)
            {
                //Color color = _selectedStructure.Sprite.TextureColor;
                //_selectedStructure.Sprite.TextureColor = Color.Black;
                //_selectedStructure.Draw(spriteBatch);
                //_selectedStructure.Sprite.TextureColor = color;

                _selectedStructureOverlay.Draw(spriteBatch, _selectedStructure.Collision.Region());
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
            foreach(Structure structure in _structures.Values)
            {
                if (structure != null)
                {
                    LoadStructureContent(structure);
                }
            }
            _selectedStructureOverlay = _spriteSheet.GetSprite("blank");
            _selectedStructureOverlay.TextureColor = ControlConstants.SELECTED_STRUCTURE_COLOR;
            _contentLoaded = true;
        }

        public void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            if (_lastMouseState.LeftButton != ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                CheckForStructureSelect(currentMouseState);
            }

            Calculate();

            foreach (Structure structure in _structures.Values)
            {
                if(structure != null)
                {
                    structure.Update(gameTime);
                }
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
            _lastMouseState = currentMouseState;
        }

        private void CheckForStructureSelect(MouseState mouseState)
        {
            if (_townZone.CollidesWith(new RectangleBody(mouseState.Position.ToVector2(), new Vector2(0))))
            {
                if(_grid.PixelToTile(mouseState.Position.X, mouseState.Position.Y, out int tileX, out int tileY))
                {
                    Structure found;
                    if(_structures.TryGetValue(CoordToIndex(tileX, tileY), out found))
                    {
                        SelectStructure(found);
                    }
                    /*
                    foreach(Structure structure in _structures)
                    {
                        if(structure.Data.X1 <= tileX && structure.Data.X2 >= tileX && structure.Data.Y1 <= tileY && structure.Data.Y2 >= tileY)
                        {
                            SelectStructure(structure);
                        }
                    }
                    */
                }
                else
                {
                    //throw new Exception("Player clicked but no tile found!");
                }

            }
        }

        private void SelectStructure(Structure structure)
        {
            if(_selectedStructure != null && _selectedStructure.Equals(structure))
            {
                _selectedStructure = null;
            }
            else
            {
                _selectedStructure = structure;
                _ghostStructure = null;
            }
        }

        private void Calculate()
        {
            // Get maximum resource count
            Dictionary<Resource.ResourceType, int> resourcesMax;
            resourcesMax = new Dictionary<Resource.ResourceType, int>();
            resourcesMax.Add(Resource.ResourceType.Wood, Config.CAPITOL_RESOURCE_ADD_WOOD);
            resourcesMax.Add(Resource.ResourceType.Stone, Config.CAPITOL_RESOURCE_ADD_STONE);
            resourcesMax.Add(Resource.ResourceType.Ore, Config.CAPITOL_RESOURCE_ADD_ORE);
            resourcesMax.Add(Resource.ResourceType.Metal, Config.CAPITOL_RESOURCE_ADD_METAL);

            // Pre-calculate
            foreach (Structure structure in _structures.Values)
            {
                if (structure != null)
                {
                    switch (structure.Data.Type)
                    {
                        case Structure.StructureType.Warehouse:
                            {
                                resourcesMax[Resource.ResourceType.Wood] += Config.WAREHOUSE_RESOURCE_ADD_WOOD;
                                resourcesMax[Resource.ResourceType.Stone] += Config.WAREHOUSE_RESOURCE_ADD_STONE;
                                resourcesMax[Resource.ResourceType.Ore] += Config.WAREHOUSE_RESOURCE_ADD_ORE;
                                resourcesMax[Resource.ResourceType.Metal] += Config.WAREHOUSE_RESOURCE_ADD_METAL;
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }


            // Add/Subtract all resources
            foreach (Structure structure in _structures.Values)
            {
                if (structure != null)
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
                                break;
                            }
                        case Structure.StructureType.Lumbermill:
                            {
                                _resources[Resource.ResourceType.Wood] += 0.15f;
                                break;
                            }
                        case Structure.StructureType.Capitol:
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
                                if (_resources[Resource.ResourceType.Ore] > 0.1f && _resources[Resource.ResourceType.Metal] < (resourcesMax[Resource.ResourceType.Metal]))
                                {
                                    _resources[Resource.ResourceType.Ore] -= 0.1f;
                                    _resources[Resource.ResourceType.Metal] += 0.05f;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
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
            for(int y = structure.Data.Y1; y <= structure.Data.Y2; y++)
            {
                for(int x = structure.Data.X1; x <= structure.Data.X2; x++)
                {
                    _structures.Add(CoordToIndex(x, y), structure);
                }
            }
            if(_contentLoaded)
            {
                LoadStructureContent(structure);
            }
        }

        public void LoadStructureContent(Structure structure, bool isGhost = false)
        {
            Sprite newSprite;
            switch (structure.Data.Type)
            {
                case Structure.StructureType.Capitol:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_CAPITOL);
                        break;
                    }
                case Structure.StructureType.House:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_HOUSE);
                        break;
                    }
                case Structure.StructureType.Forge:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_FORGE);
                        break;
                    }
                case Structure.StructureType.Lumbermill:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_LUMBERMILL);
                        break;
                    }
                case Structure.StructureType.Warehouse:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_WAREHOUSE);
                        break;
                    }
                case Structure.StructureType.Mine:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_MINE);
                        break;
                    }
                case Structure.StructureType.Road:
                    {
                        newSprite = _spriteSheet.GetSprite(Config.STRUCTURE_TEXTURE_ROAD);
                        break;
                    }
                case Structure.StructureType.Other:
                    {
                        newSprite = _spriteSheet.GetSprite("structure-" + structure.Data.Width + 'x' + structure.Data.Height);
                        break;
                    }
                default:
                    {
                        throw new Exception("Structure does not have a valid structure type");
                    }
            }
            //Sprite newSprite = _spriteSheet.GetSprite("structure-" + structure.Data.Width + 'x' + structure.Data.Height);
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
                    case Structure.StructureType.Capitol:
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
                    case Structure.StructureType.Road:
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
                Structure newPlacedStructure;
                if (structure.Data.Type == Structure.StructureType.Road)
                {
                    newPlacedStructure = new Road(_grid, structure.Data, _spriteSheet);
                }
                else
                {
                    newPlacedStructure = _ghostStructure.ToStructure();
                }
                //LoadStructureContent(newPlacedStructure, false);
                //_structures.Add(newPlacedStructure);
                AddStructure(newPlacedStructure);
                _ghostStructure = null;
                if(newPlacedStructure.Data.Type == Structure.StructureType.Road)
                {
                    ((Road)newPlacedStructure).RecalculateTexture(this);

                    int x = newPlacedStructure.Data.X1;
                    int y = newPlacedStructure.Data.Y1;
                    // Top side
                    if (y != 0)
                    {
                        if (IsStructureUnderTile(x, y - 1))
                        {
                            Structure found = GetStructureUnderTile(x, y - 1);
                            if (found.Data.IsRoad)
                                ((Road)found).RecalculateTexture(this);
                        }
                    }

                    // Right side
                    if (x < _grid.Info.TilesWide - 1)
                    {
                        if (IsStructureUnderTile(x + 1, y))
                        {
                            Structure found = GetStructureUnderTile(x + 1, y);
                            if (found.Data.IsRoad)
                                ((Road)found).RecalculateTexture(this);
                        }
                    }

                    // Bottom side
                    if (y < _grid.Info.TilesHigh -1)
                    {
                        if (IsStructureUnderTile(x, y + 1))
                        {
                            Structure found = GetStructureUnderTile(x, y + 1);
                            if (found.Data.IsRoad)
                                ((Road)found).RecalculateTexture(this);
                        }
                    }

                    // Left side
                    if (x != 0)
                    {
                        if (IsStructureUnderTile(x - 1, y))
                        {
                            Structure found = GetStructureUnderTile(x - 1, y);
                            if (found.Data.IsRoad)
                                ((Road)found).RecalculateTexture(this);
                        }
                    }
                }
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

        public bool IsStructureUnderTile(int tileX, int tileY)
        {
            if (GetStructureUnderTile(tileX, tileY) == null)
                return false;
            else
                return true;
        }

        public Structure GetStructureUnderTile(int tileX, int tileY)
        {
            if(_structures.TryGetValue(CoordToIndex(tileX, tileY), out Structure structure))
            {
                return structure;
            }
            return null;
            /*
            foreach (Structure structure in _structures)
            {
                if (structure.Data.X1 <= tileX && structure.Data.X2 >= tileX && structure.Data.Y1 <= tileY && structure.Data.Y2 >= tileY)
                {
                    return structure;
                }
            }
            return null;
            */
        }

        public void SetResourceCount(Resource.ResourceType type, int count)
        {
            if (_resources.ContainsKey(type))
            {
                if (_resources[type] + count > Config.MAX_RESOURCE_VALUE_LABEL)
                    _resources[type] = Config.MAX_RESOURCE_VALUE_LABEL;
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
                if (_resources[type] + count > Config.MAX_RESOURCE_VALUE_LABEL)
                    _resources[type] = Config.MAX_RESOURCE_VALUE_LABEL;
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
            _selectedStructure = null;
        }
        public void RemoveStructureButton_Click(object sender, EventArgs e)
        {
            RemoveStructure(_selectedStructure);
        }

        private void RemoveStructure(Structure structure)
        {
            if (structure != null)
            {
                for (int y = structure.Data.Y1; y <= structure.Data.Y2; y++)
                {
                    for (int x = structure.Data.X1; x <= structure.Data.X2; x++)
                    {
                        _structures.Remove(CoordToIndex(x, y));
                    }
                }
            }
            _selectedStructure = null;
        }

        public int CoordToIndex(int x, int y)
        {
            if (x >= _grid.Info.TilesWide || y >= _grid.Info.TilesHigh || x < 0 || y < 0)
            {
                throw new Exception("Invalid coordinates");
            }
            return y * _grid.Info.TilesWide + x;
        }

        public bool IndexToCoords(int index, out int x, out int y)
        {
            y = index / _grid.Info.TilesWide;
            x = index % _grid.Info.TilesWide;

            if (index < 0 || index >= _grid.Info.TilesWide * _grid.Info.TilesHigh)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
