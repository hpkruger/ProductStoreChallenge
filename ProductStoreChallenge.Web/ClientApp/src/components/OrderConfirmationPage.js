import React, { Component } from 'react';
import { Row, Col } from 'reactstrap';

export class OrderConfirmationPage extends Component {
  static displayName = "Thank you";


  render() {
    return (
        <Row className="justify-content-center">
            <h1>Thank you for your order</h1>
        </Row>
    );
  }
}
