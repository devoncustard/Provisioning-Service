﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="enc")]
public partial class Enc : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertNodes(Nodes instance);
  partial void UpdateNodes(Nodes instance);
  partial void DeleteNodes(Nodes instance);
  #endregion
	
	public Enc(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Enc(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Enc(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public Enc(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Nodes> Nodes
	{
		get
		{
			return this.GetTable<Nodes>();
		}
	}
	
	public System.Data.Linq.Table<Nodes1> Nodes1
	{
		get
		{
			return this.GetTable<Nodes1>();
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.nodes")]
public partial class Nodes : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private string _Node;
	
	private string _Enctext;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNodeChanging(string value);
    partial void OnNodeChanged();
    partial void OnEnctextChanging(string value);
    partial void OnEnctextChanged();
    #endregion
	
	public Nodes()
	{
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="node", Storage="_Node", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
	public string Node
	{
		get
		{
			return this._Node;
		}
		set
		{
			if ((this._Node != value))
			{
				this.OnNodeChanging(value);
				this.SendPropertyChanging();
				this._Node = value;
				this.SendPropertyChanged("Node");
				this.OnNodeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="enctext", Storage="_Enctext", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
	public string Enctext
	{
		get
		{
			return this._Enctext;
		}
		set
		{
			if ((this._Enctext != value))
			{
				this.OnEnctextChanging(value);
				this.SendPropertyChanging();
				this._Enctext = value;
				this.SendPropertyChanged("Enctext");
				this.OnEnctextChanged();
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.nodes1")]
public partial class Nodes1
{
	
	private string _Hostname;
	
	private System.Nullable<System.Guid> _Id;
	
	private string _Classes;
	
	private string _Environment;
	
	private string _Parameters;
	
	public Nodes1()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="hostname", Storage="_Hostname", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
	public string Hostname
	{
		get
		{
			return this._Hostname;
		}
		set
		{
			if ((this._Hostname != value))
			{
				this._Hostname = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="id", Storage="_Id", DbType="UniqueIdentifier")]
	public System.Nullable<System.Guid> Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this._Id = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="classes", Storage="_Classes", DbType="NVarChar(100)")]
	public string Classes
	{
		get
		{
			return this._Classes;
		}
		set
		{
			if ((this._Classes != value))
			{
				this._Classes = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="environment", Storage="_Environment", DbType="NVarChar(50)")]
	public string Environment
	{
		get
		{
			return this._Environment;
		}
		set
		{
			if ((this._Environment != value))
			{
				this._Environment = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Name="parameters", Storage="_Parameters", DbType="NVarChar(100)")]
	public string Parameters
	{
		get
		{
			return this._Parameters;
		}
		set
		{
			if ((this._Parameters != value))
			{
				this._Parameters = value;
			}
		}
	}
}
#pragma warning restore 1591
