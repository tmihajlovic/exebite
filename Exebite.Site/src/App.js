import React, { Component } from 'react'
import './index.css'
import Table from './Table'
import Form from './Form'

class App extends Component {
  state = {
    data: [],
  }

  removeRow = index => {
    const { data } = this.state

    this.setState({
      data: data.filter((row, i) => {
        return i !== index
      }),
    })
  }

  componentDidMount() {
    const url =
      'http://localhost:6879/api/food/Query?Page=1&Size=100';

    fetch(url, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
      .then(result => result.json())
      .then(result => {
        this.setState({
          data: result.items
        })
      })
      .catch(err => console.log(err))
  }


  render() {
    const { data } = this.state

    return (
      <div className="App">
        <h1>Hello, Exebite!</h1>
        <div className="container">
          <h2>Foods:</h2>
          <Table data={data} removeRow={this.removeRow} />
        </div>
      </div>
    )
  }
}

export default App