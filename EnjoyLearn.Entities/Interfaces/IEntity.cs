﻿namespace EnjoyLearn.Models.Interfaces
{
  public interface IEntity<TId>
  {
    TId Id { get; set; }
  }
}
