// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.1
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AvroConsole.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.11.1")]
	public partial class Vehicle : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"Vehicle\",\"namespace\":\"AvroConsole.Entity\",\"fields\":[{\"na" +
				"me\":\"registration\",\"type\":\"string\"},{\"name\":\"speed\",\"type\":\"int\"},{\"name\":\"coord" +
				"inates\",\"type\":\"string\"}]}");
		private string _registration;
		private int _speed;
		private string _coordinates;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return Vehicle._SCHEMA;
			}
		}
		public string registration
		{
			get
			{
				return this._registration;
			}
			set
			{
				this._registration = value;
			}
		}
		public int speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				this._speed = value;
			}
		}
		public string coordinates
		{
			get
			{
				return this._coordinates;
			}
			set
			{
				this._coordinates = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.registration;
			case 1: return this.speed;
			case 2: return this.coordinates;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.registration = (System.String)fieldValue; break;
			case 1: this.speed = (System.Int32)fieldValue; break;
			case 2: this.coordinates = (System.String)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
