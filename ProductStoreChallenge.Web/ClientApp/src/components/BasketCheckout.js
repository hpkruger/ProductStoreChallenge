import React, { Component } from 'react';
import { Row, Col, Button, Input  } from 'reactstrap';

export class BasketCheckout extends Component {
    constructor(props) {
        super(props);

        this.state = {
            loading: true,
            subTotalAmount: 0,
            shippingAmount: 0,
            totalAmount: 0
        };
    }
    componentDidMount() {
        // Hans: Run side effects should only be done in the "Commit" phase in the React pipeline - hence doing network calls in componentDidMount() and componentDidUpdate() is fine
        this.calculateTotalAmount(this.props.items);
    }
    componentDidUpdate(prevProps) {
        let basketChanged = JSON.stringify(prevProps.items) !== JSON.stringify(this.props.items);

        // Hans: recalc the amounts if basket items have changed
        if (basketChanged) {
            this.calculateTotalAmount(this.props.items);
        }

    }
    calculateTotalAmount = async (items) => {

        console.log(`calculateTotalAmount() ${JSON.stringify(items)}`);
        try {
            this.setState({ loading: true });

            const response = await fetch('/Calculations/Shipping', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ items: items.map(item => { return { ProductId: item.product.id, Qty: item.qty } }) })
            });

            const subTotalAmount = this.props.items.reduce((accumulator, currentValue) => accumulator + (currentValue.product.price * currentValue.qty), 0);
            const shippingAmount = await response.json();
            const totalAmount = subTotalAmount + shippingAmount;

            this.setState({ loading: false, subTotalAmount, shippingAmount, totalAmount });
        }
        catch (err) {
            console.log(`calculateTotalAmount() failed. Error: ${err.message}`);
        }
    }
    handleCheckoutBtn = async (e) => {

        console.log(`handleCheckoutBtn()`);
        try {
            this.setState({ loading: true });

            const response = await fetch('/Orders/', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ items: this.props.items.map(item => { return { ProductId: item.product.id, Qty: item.qty } }) })
            });

            window.location.href = '/OrderConfirmation';
        }
        catch (err) {
            console.log(`handleCheckoutBtn() failed. Error: ${err.message}`);
        }
    }

    render() {
        let content = this.state.loading ? <p>Loading...</p> : 
            <React.Fragment>
                <p>
                    SubTotal: AUD ${this.state.subTotalAmount}
                </p>
                <p>
                    Shipping: AUD ${this.state.shippingAmount}
                </p>
                <hr />
                <p>
                    Total: AUD ${this.state.totalAmount}
                </p>
                <Button className="btn btn-primary" onClick={this.handleCheckoutBtn}>Place Order</Button>

            </React.Fragment>

        return (
            <Row>
                <Col xs="12">
                    <hr />
                    {content}
                </Col>
            </Row>
        );
    }
}
