import React from 'react';

export default class LoanCalculator extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loanPrincipal: 10000,
            loanTerm: 10,
            loanInterest: 14.5,
            weeklyRepayment: null,
            monthlyRepayment: null,
            fullTermRepayment: null
        };
        this.handleInput = this.handleInput.bind(this);
        this.getRepaymentCalculations = this.getRepaymentCalculations.bind(this);
    }

    handleInput(e) {
        this.setState({
            [e.target.name]: e.target.value,
            weeklyRepayment: null,
            monthlyRepayment: null,
            fullTermRepayment: null
        });
    }

    getRepaymentCalculations() {
        let self = this;
        let loanData = [{
                LoanPrincipal: parseInt(this.state.loanPrincipal),
                LoanTerm: parseInt(this.state.loanTerm),
                LoanInterestRate: parseFloat(this.state.loanInterest)
        }];
        console.log(loanData);
        console.log(JSON.stringify(loanData));
        fetch(`http://localhost:62077/api/loan/getLoanRepayments`, {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify( loanData )
        })
        .then(function (response) {
            return response.json();
        })
        .then(function (myJson) {
            console.log(myJson);
            self.setState({
                weeklyRepayment: myJson.repaymentsCalculated[0].weeklyRepayment,
                monthlyRepayment: myJson.repaymentsCalculated[0].monthlyRepayment,
                fullTermRepayment: myJson.repaymentsCalculated[0].fullTermRepayment
            });
        });
    }

    render() {
		//Initialise some statements e.g. let { <property1>, <property2> } = this.props;
		return (
            <div>
                <div style={{ order: 0, textAlign: 'center' }} className="Rtable-cell Rtable-cell--head">
                    <h3> Loan </h3>
                </div>
                <div style={{ order: 1 }} className="Rtable-cell"> Loan Principal: ${this.state.loanPrincipal}
                    <input type="range" onChange={this.handleInput} min="1000" max="20000" step="500" value={this.state.loanPrincipal} className="slider" name="loanPrincipal"/>
                </div>
                <div style={{ order: 2 }} className="Rtable-cell"> Loan Term: {this.state.loanTerm} months
                    <input type="range" onChange={this.handleInput} min="6" max="60" value={this.state.loanTerm} className="slider" name="loanTerm"/>
                </div>
                <div style={{ order: 3 }} className="Rtable-cell"> Loan Interest: {this.state.loanInterest}%
                    <input type="range" onChange={this.handleInput} min="8" max="20" step="0.05" value={this.state.loanInterest} className="slider" name="loanInterest" />
                </div>
                <div style={{ order: 4, textAlign: 'center' }} className="Rtable-cell Rtable-cell--foot">
                    <button onClick={this.getRepaymentCalculations}>Calculate Repayments</button>
                </div>
                <div style={{ order: 5 }} className="Rtable-cell Rtable-cell--foot">
                    <strong>Weekly Repayment: {this.state.weeklyRepayment > 0 ? `$${this.state.weeklyRepayment}` : null}</strong>
                </div>
                <div style={{ order: 6 }} className="Rtable-cell Rtable-cell--foot">
                    <strong>Monthly Repayment: {this.state.monthlyRepayment > 0 ? `$${this.state.monthlyRepayment}` : null}</strong>
                </div>
                <div style={{ order: 7, marginBottom: '20px' }} className="Rtable-cell Rtable-cell--foot">
                    <strong>Full-term Repayment: {this.state.fullTermRepayment > 0 ? `$${this.state.fullTermRepayment}` : null}</strong>
                </div>
            </div>
        );
    }
}