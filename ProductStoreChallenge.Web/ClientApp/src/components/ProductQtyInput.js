import React, { Component } from 'react';
import { Row, Col, Button, Input  } from 'reactstrap';

export class ProductQtyInput extends Component {
    constructor(props) {
        super(props);
        this.state = {
            qty: this.props.qty
        };
    }
    handleDecrementQty = (e) => {
        this.setState(
            prevState => ({
                qty: prevState.qty > 0 ? Number(prevState.qty) - 1 : 0
            }),
            () => {
                this.props.onQtyChanged(this.state.qty);
            });
        e.preventDefault();
    }
    handleIncrementQty = (e) => {
        this.setState(
            prevState => ({
                qty: Number(prevState.qty) + 1
            }),
            () => {
                this.props.onQtyChanged(this.state.qty);
            });
        e.preventDefault();
    }
    handleQtyChanged = (e) => {
        let qty = Number(e.target.value);
        this.setState({
            qty: qty >= 0 ? qty : 0
        }, () => {
            this.props.onQtyChanged(this.state.qty);
        });
        e.preventDefault();
    }
    render () {
        return (
            <Row>
                <Col xs="2" />
                <Col xs="2">
                    <Button className="btn btn-primary" onClick={this.handleDecrementQty}>-</Button>
                </Col>
                <Col xs="1" />
                <Col xs="3">
                    <Input type="text" value={this.state.qty} onChange={this.handleQtyChanged}></Input>
                </Col>
                <Col xs="2">
                    <Button className="btn btn-primary" onClick={this.handleIncrementQty}>+</Button>
                </Col>
                <Col xs="2" />
            </Row>
        );
    }
}
