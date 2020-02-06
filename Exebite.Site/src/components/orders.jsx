import React, { Component } from "react";
import Order from "./order";

class Orders extends Component {
  render() {
    const { orders, onRemoveFromOrder } = this.props;
    return (
      <div>
        <h3>To order:</h3>
        {orders.map((order, index) => {
          return (
            <Order
              key={index}
              order={order}
              onRemoveFromOrder={onRemoveFromOrder}
            />
          );
        })}
        <h3>
          Order price: {orders.reduce((total, food) => total + food.price, 0)}
        </h3>
        <p>Location for delivery: this should be dropdown</p>
        {/* <button>Submit order</button> */}
      </div>
    );
  }
}

export default Orders;
