import React, { Component } from "react";

class Food extends Component {
  render() {
    const { food, onAddToOrder } = this.props;
    return (
      <div>
        <span>{food.name}</span>
        <span> - </span>
        <span>{food.price}</span>
        <span> - </span>
        <span>{food.description}</span>
        <button onClick={() => onAddToOrder(food)}>+</button>
      </div>
    );
  }
}

export default Food;
