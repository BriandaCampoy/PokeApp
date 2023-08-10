using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.Models
{
    public class PokemonDetails
    {
        public List<AbilitySlot> Abilities { get; set; }
        public int BaseExperience { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string LocationAreaEncounters { get; set; }

        public List<MoveInfo> Moves { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public Species Species { get; set; }

        public Sprites Sprites { get; set; }

        public List<StatInfo> Stats { get; set; }

        public List<PokemonTypeSlot> Types { get; set; }
        public int Weight { get; set; }
    }

    public class AbilitySlot
    {
        public Ability Ability { get; set; }
        public bool IsHidden { get; set; }
        public int Slot { get; set; }
    }

    public class Ability
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }


    public class MoveInfo
    {
        public Move Move { get; set; }
        public List<VersionGroupDetail> VersionGroupDetails { get; set; }
    }

    public class Move
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class VersionGroupDetail
    {
        public int LevelLearnedAt { get; set; }
        public MoveLearnMethod MoveLearnMethod { get; set; }
        public VersionGroup VersionGroup { get; set; }
    }

    public class MoveLearnMethod
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class VersionGroup
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Species
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class Sprites
    {
        public string back_default { get; set; }
        public string back_female { get; set; }
        public string back_shiny { get; set; }
        public string back_shiny_female { get; set; }
        public string front_default { get; set; }
        public string front_female { get; set; }
        public string front_shiny { get; set; }
        public string front_shiny_female { get; set; }
        public IEnumerable<string> Properties
        {
            get
            {
                List<string> properties = new List<string>();

                if (!string.IsNullOrEmpty(back_default))
                    properties.Add(back_default);

                if (!string.IsNullOrEmpty(back_female))
                    properties.Add(back_female);

                if (!string.IsNullOrEmpty(back_shiny))
                    properties.Add(back_shiny);

                if (!string.IsNullOrEmpty(back_shiny_female))
                    properties.Add(back_shiny_female);

                if (!string.IsNullOrEmpty(front_default))
                    properties.Add(front_default);

                if (!string.IsNullOrEmpty(front_female))
                    properties.Add(front_female);

                if (!string.IsNullOrEmpty(front_shiny))
                    properties.Add(front_shiny);

                if (!string.IsNullOrEmpty(front_shiny_female))
                    properties.Add(front_shiny_female);

                return properties;
            }
        }
    }

    public class StatInfo
    {
        public int base_stat { get; set; }
        public int Effort { get; set; }
        public Stat Stat { get; set; }
    }

    public class Stat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class PokemonTypeSlot
    {
        public int Slot { get; set; }
        public Type Type { get; set; }
    }

    public class Type
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
