import React, { Component } from 'react'
import './index.css'
import DailyMenus from './components/dailyMenus';
import Orders from './components/orders';

class App extends Component {

  state = {
    dailyMenus: [],
    orders: []
  };

  componentDidMount() {
    const dailyMenuUrl =
      'http://localhost:6879/api/dailymenu/Query?Page=1&Size=100';

    fetch(dailyMenuUrl)
      .then(result => result.json())
      .then(result => {
        this.setState({
          dailyMenus: result.items
        })
      })
      .catch(err => console.log(err))
  }

  handleAddToOrder = food => {
    this.setState({ orders: this.state.orders.concat(food) })
  }

  handlerRemoveFromOrder = foodId => {
    this.setState({ orders: this.state.orders.filter(f => f.id !== foodId) })
  }

  orderSubmitted = () => {
    this.setState({ orders: [] })
  }

  render() {
    const { dailyMenus, orders } = this.state

    return (
      <React.Fragment>
        <h1>Welcome to ExeBite!</h1>
        <main className="container">
          <Orders orders={orders} onRemoveFromOrder={this.handlerRemoveFromOrder} onOrderSubmitted={this.orderSubmitted} />
          <DailyMenus
            dailyMenus={dailyMenus}
            onAddToOrder={this.handleAddToOrder}
            onRemoveFromOrder={this.handlerRemoveFromOrder} />
        </main>
      </React.Fragment >
    )
  }
}

export default App