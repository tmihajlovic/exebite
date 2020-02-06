import React, { Component } from "react";
import Food from "./food";

class DailyMenu extends Component {
  render() {
    const { dailyMenu, onAddToOrder } = this.props;

    return (
      <div>
        <h2>Daily menu</h2>
        {dailyMenu.foods.map(food => (
          <Food key={food.id} food={food} onAddToOrder={onAddToOrder} />
        ))}
      </div>
    );
  }
}

export default DailyMenu;
