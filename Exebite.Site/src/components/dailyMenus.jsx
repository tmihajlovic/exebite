import React, { Component } from "react";
import DailyMenu from "./dailyMenu";

class DailyMenus extends Component {
  render() {
    const { dailyMenus, onAddToOrder } = this.props;
    return (
      <div>
        {dailyMenus.map(dailyMenu => (
          <DailyMenu
            key={dailyMenu.id}
            dailyMenu={dailyMenu}
            onAddToOrder={onAddToOrder}
          />
        ))}
      </div>
    );
  }
}

export default DailyMenus;
