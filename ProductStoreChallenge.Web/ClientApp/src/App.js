import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { ProductPage } from './components/ProductPage';
import { BasketPage } from './components/BasketPage';
import { OrderConfirmationPage } from './components/OrderConfirmationPage';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;
    constructor(props) {
        super(props);

        // Hans: We capture the state of the basket at the root component (App) which means that each child component (ProductQtyInput -> Product -> ProductPage -> App)
        // has to propagate up the onQtyChanged event until it has reached the root component (App). 
        // Now, this is cumbersome and one of the reasons why people use other libraries such as Redux for global state management which allows to dispatch events from every component in the hierarchy
        // For this challenge though I decided to just stick to pure React without Redux.
        this.state = {
            basket: { items: [] }
        }
    }
    handleQtyChanged = (product, qty) => {

        this.setState(prevState => {
            let items = Object.assign([], prevState.basket.items);
            let index = items.findIndex((item) => {
                return item.product.id === product.id;
            });
            if (index > -1) {
                if (qty === 0) {
                    items.splice(index, 1); // delete
                }
                else {
                    items[index].qty = qty; // update
                }
            }
            else {
                items.push({ product, qty }); //new
            }
            return { basket: { items } };
        }, () => {
                console.log(`Basket: ${JSON.stringify(this.state.basket.items)}`);
        });
    }
    render() {
        return (
            <Layout basketItemCount={this.state.basket.items.length}>
                <Route exact path='/' render={(routeProps) => (
                    <ProductPage onQtyChanged={this.handleQtyChanged} items={this.state.basket.items} />
                )} />
    
                <Route path='/Basket' render={(routeProps) => (
                    <BasketPage onQtyChanged={this.handleQtyChanged} items={this.state.basket.items} />
                )} />
                <Route path='/OrderConfirmation' component={OrderConfirmationPage} />
            </Layout>
        );
    }
}
