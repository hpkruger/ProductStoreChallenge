import React, { Component } from 'react';
import { Row, Col, Button, Input  } from 'reactstrap';

export class BasketTotal extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: this.props.items
        };
    }

    render () {
        return (
            <Row>
                <Col xs="12">
                    <hr />
                    <p>
                        Subtotal: AUD ${this.state.items.reduce((accumulator, currentValue) => accumulator + (currentValue.product.price * currentValue.qty), 0)}
                    </p>
                    <p>
                        Shipping: Todo
                        </p>

                </Col>
            </Row>
        );
    }
}
