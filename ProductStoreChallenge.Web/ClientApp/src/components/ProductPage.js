import React, { Component } from 'react';
import { Row, Col } from 'reactstrap';

import { Product } from './Product'

export class ProductPage extends Component {
    static displayName = "Products";
    constructor(props) {
        super(props);

        this.state = { products: [], loading: true}

    }
    componentDidMount() {
        this.fetchProducts();
    }
    // Hans: I prefer using async/await as opposed to chaining Javascript promises as it makes reading asynchronous code easier
    async fetchProducts() {
        try {
            const response = await fetch('/Products/');
            const data = await response.json();
            this.setState({ products: data, loading: false });
        }
        catch (err) {
            console.log(`fetchProducts() failed. Error: ${err.message}`);
        }
    }

    // Hans: Note the usage of arrow functions here as opposed to the "function" keyword. Arrow functions have the advantage of automatically scoping to "this" which
    // makes "bind()" or storing a reference to "this" in variables such as "self" unnecessary  
    handleQtyChanged = (product, qty) => {
        console.log(`Product: ${JSON.stringify(product)}, Qty: ${qty}`);
        this.props.onQtyChanged(product, qty);
    }

    getQtyInBasket = (productId) => {
        let index = this.props.items.findIndex((item) => {
            return item.product.id === productId;
        });
        if (index > -1) {
            return this.props.items[index].qty;
        }
        return 0;
    }

    render() {
        let content = this.state.loading ? <p>Loading...</p> :
            this.state.products.map((product) =>
                <Col xs="4">
                    <Product key={product.id} value={product} onQtyChanged={this.handleQtyChanged} qty={this.getQtyInBasket(product.id)} / >
                </Col>
            );

        return (
            <React.Fragment>
                <Row className="justify-content-center">
                    <h1>Welcome to our store!</h1>
                </Row>
                <Row className="justify-content-center">
                    <h4>Please select products from our large sortiment below</h4>
                </Row>
                <hr />
                <Row>
                    {content}
                </Row>
            </React.Fragment>
        );
    }
}
