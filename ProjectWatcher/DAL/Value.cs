﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Interface;
using DAL.Helpers;

namespace DAL
{
    partial class Value: IValue
    {
        /// <summary>
        /// Gets all nessesary parameters and creates entity of "Value" class without id.
        /// </summary>
        /// <param name="systemName"></param>
        /// <param name="projectId"></param>
        /// <param name="visible"></param>
        /// <returns>Entity not saved in DB.</returns>
        public static Value CreateValue(String systemName, int projectId, bool visible, bool important, String author)
        {
            Property property = ConnectionHelper.GetProperty(systemName);
            if (property == null)
            {
                return null; 
            }
            return new Value { SystemName = systemName, ProjectId = projectId, Visible = visible,
                Value1 = property.DefaultValue().ToString(), Important = important,
                Author = author, Time = DateTime.Now
            };
        }

        internal static Value CreateValue(Value original)
        {
            return (Value)original.MemberwiseClone();
        }

        internal Value(IValue original)
        {
            this.ConvertFromUserInput(original);
        }

        internal Value()
        { }

        public Object GetValue()
        {
            return Value1;
        }

        public void SetValue(Object value)
        {
            this.Value1 = Helpers.EntityHelper.FormatForDB(value, new Property {Type = this.Property.Type}).ToString();
        }

        public IProperty GetProperty()
        {
            return Property;
        }

        public void SetProperty(IProperty input)
        {
            Property = new Property(input);
        }

        public IProject GetProject()
        {
            return Project;
        }

        public IEnumerable<IHistory> GetHistories()
        {
            return Histories;
        }

        public IValue GetCopy()
        {
            return (Value)this.MemberwiseClone();
        }

        public override int GetHashCode()
        {
            return (Id + Time.Date.DayOfYear)%Int32.MaxValue;
        }

        public override bool Equals(object obj)
        {
            Value other = obj as Value;
            if( other == null)
            {
                return false;
            }
            return Id == other.Id && Time == other.Time && Value1 == other.Value1 && Author == other.Author &&
                   Important == other.Important && Visible == other.Visible;
        }



        internal void ModifyItself(Value original)
        {
            this.Important = original.Important;
            this.SystemName = original.SystemName;
            this.Value1 = original.Value1;
            this.ProjectId = original.ProjectId;
            this.Id = original.Id;
            this.Visible = original.Visible;
            this.Author = original.Author;
            this.Time = original.Time;
        }



        internal void ConvertFromUserInput(IValue original)
        {
            this.Important = original.Important;
            this.SystemName = original.SystemName;
            this.Value1 = EntityHelper.FormatForDB(original).ToString();
            this.ProjectId = original.ProjectId;
            this.Id = original.Id;
            this.Visible = original.Visible;
            this.Author = original.Author;
            this.Time = original.Time;
        }







    }
}
