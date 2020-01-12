import React, { Component } from 'react';
import { Row, Col, Button } from 'reactstrap';

import { Product } from './Product'
import { BasketCheckout } from './BasketCheckout'

export class BasketPage extends Component {
    static displayName = "Basket";

    handleQtyChanged = (product, qty) => {
        this.props.onQtyChanged(product, qty);
    }

    render() {
        let content = this.props.items.length === 0 ? <p>Basket is empty</p> :
            this.props.items.map((item) =>
                <Col xs="4">
                    <Product key={item.product.id} value={item.product} onQtyChanged={this.handleQtyChanged} qty={item.qty}/>
                </Col>
            );

        return (
            <React.Fragment>
                <Row className="justify-content-center">
                    <h1>Your Basket</h1>
                </Row>
                <hr />
                <Row>
                    {content}
                </Row>

                {this.props.items.length > 0 && <BasketCheckout items={this.props.items} />}

            </React.Fragment>
        );
    }
}
