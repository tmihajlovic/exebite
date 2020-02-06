import React, { Component } from "react";

class Order extends Component {
  render() {
    const { order, onRemoveFromOrder } = this.props;

    return (
      <div>
        <span>
          {order.name} - {order.price}
        </span>
        <button onClick={() => onRemoveFromOrder(order.id)}>X</button>
      </div>
    );
  }
}

export default Order;
