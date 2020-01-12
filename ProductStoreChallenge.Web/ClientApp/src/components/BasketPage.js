import React, { Component } from 'react';
import { Row, Col } from 'reactstrap';

import { Product } from './Product'
import { BasketTotal } from './BasketTotal'

export class BasketPage extends Component {
    static displayName = "Basket";
    constructor(props) {
        super(props);

        this.state = { items: this.props.items}
    }

    handleQtyChanged = (product, qty) => {
        this.props.onQtyChanged(product, qty);
    }

    render() {
        let content = this.state.items.length === 0 ? <p>Basket is empty</p> :
            this.state.items.map((item) =>
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
                <BasketTotal items={this.state.items}/>
            </React.Fragment>
        );
    }
}
