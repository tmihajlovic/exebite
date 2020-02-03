import React from 'react'

const TableHeader = () => {
  return (
    <thead>
      <tr>
        <th>Name</th>
        <th>Price</th>
        <th>Description</th>
        <th>Restaurant</th>
      </tr>
    </thead>
  )
}

const TableBody = props => {
  const rows = props.data.map((row, index) => {
    return (
      <tr key={index}>
        <td>{index + 1}</td>
        <td>{row.name}</td>
        <td>{row.price}</td>
        <td>{row.description}</td>
        <td>{row.restaurantId}</td>
        <td>
          <button onClick={() => props.removeRow(index)}>Delete</button>
        </td>
      </tr>
    )
  })

  return <tbody>{rows}</tbody>
}
const Table = (props) => {
  const { data, removeRow } = props;

  return (
    <table>
      <TableHeader />
      <TableBody data={data} removeRow={removeRow} />
    </table>
  )
}

export default Table