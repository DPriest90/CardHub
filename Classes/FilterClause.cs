using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardHub.Classes
{
    public class FilterClause
    {
        // The name of the data field to apply the filter on (e.g., "Level", "Attribute", "ATK")
        public string Field { get; set; }

        // The comparison or logical operator used in the filter clause.
        // Examples: "=", "<", ">", ">=", "<=", "Contains"
        public string Operator { get; set; }

        // The value to compare against using the specified operator
        public string Value { get; set; }

        // Returns a human-readable representation of the filter clause
        public override string ToString() => $"{Field} {Operator} {Value}";

        // Validates whether the filter clause is structurally sound and semantically usable
        public bool ValidateFilter()
        {
            // Placeholder logic—implement actual validation later
            return false;
        }
    }
}
