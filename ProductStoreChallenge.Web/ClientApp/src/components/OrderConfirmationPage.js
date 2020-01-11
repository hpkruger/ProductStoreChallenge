import React, { Component } from 'react';

export class OrderConfirmationPage extends Component {
  static displayName = "Thank you";

  constructor(props) {
    super(props);
    this.state = { };
  }

  componentDidMount() {
  }

  render() {
    return (
      <div>
        <h1>Thank you for your order</h1>
      </div>
    );
  }
}
