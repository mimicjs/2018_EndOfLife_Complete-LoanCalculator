import React from 'react';
import LoanCalculator from './LoanCalculator.jsx';

export default class LoanCalculatorTable extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            calculatorsActive: 5
        };
        this.handleInput = this.handleInput.bind(this);
    }

    handleInput(e) {
        this.setState({ [e.target.name]: e.target.value });
    }
    
    //to-do: Map data from URL else from state and perhaps from cookies 
    //          for number of loancalculator components
    
    render() {
        //Initialise some statements e.g. let { <property1>, <property2> } = this.props;
        return (
            <div>
                <h2 style={{ textAlign: 'center' }}>Calculate your loan repayments today!</h2>
                <h5 style={{ textAlign: 'center' }}>
                    Our loaning services require a $240 one-off application fee + $1.80 per month service fee <br/>
                    For loan amounts greater than $20,000 please contact us on 0800 --- ---. <br/>
                    * Repayments shown below are indicative only and may vary upon application
                 </h5>
                <div className={`Rtable Rtable--${this.state.calculatorsActive}cols Rtable--collapse`}>

                    <LoanCalculator />
                    <LoanCalculator />
                    <LoanCalculator />
                    <LoanCalculator />
                    <LoanCalculator />

                </div>
            </div>
        );
    }
}