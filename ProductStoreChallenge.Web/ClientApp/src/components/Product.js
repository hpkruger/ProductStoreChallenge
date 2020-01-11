import React, { Component } from 'react';
import { Row, Col } from 'reactstrap';

import { ProductQtyInput } from './ProductQtyInput'

export class Product extends Component {
    constructor(props) {
        super(props);
        this.state = {
            qty: this.props.qty ? this.props.qty : 0
        };

    }
    handleQtyChanged = (qty) => {
        this.props.onQtyChanged(this.props.value, qty);
    }
    render() {
        return (
            <React.Fragment>
                <Row>
                    <Col xs="auto">
                        <img className="img-thumbnail"
                            src={`/images/${this.props.value.id}.jpg`}
                            alt={this.props.value.name}
                        />
                    </Col>
                </Row>
                <Row className="justify-content-center">
                    <Col xs="auto">
                        {this.props.value.name}
                    </Col>
                </Row>
                <Row className="justify-content-center">
                    <Col xs="auto">
                        {`AUD $${this.props.value.price}`}
                    </Col>
                </Row>
                <Row className="mb-5 mt-3">
                    <Col xs="12">
                        <ProductQtyInput onQtyChanged={this.handleQtyChanged} qty={this.state.qty}/>
                    </Col>
                </Row>
            </React.Fragment>
        );
    }
}
