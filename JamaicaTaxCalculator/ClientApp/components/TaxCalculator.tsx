import * as React from 'react';
import { RouteComponentProps } from 'react-router';

interface CalculatorInput {
    grossSalary: number,
    salaryResponse: SalaryInformationResponse,
    salaryDataPresent: boolean
}

interface SalaryDetails {
    grossIncome: number,
    pension: number,
    nis: number,
    educationTax: number,
    nht: number,
    totalStatutoryDeductions: number,
    incomeTaxThreshold: number,
    taxableIncome: number,
    incomeTax: number,
    netIncome: number
}

interface SalaryInformationResponse {
    success: boolean,
    details: SalaryDetails,
    errors: string[]
}

export class TaxCalculator extends React.Component<RouteComponentProps<{}>, CalculatorInput> {
    constructor() {
        super();
        this.state = {
            grossSalary: 0,
            salaryResponse: this.initializeSalaryDetails(),
            salaryDataPresent: false
        };
        this.updateGrossSalary = this.updateGrossSalary.bind(this);
    }

    initializeSalaryDetails() {
        return {
            success: false,
            errors: [],
            details: {
                grossIncome: 0,
                pension: 0,
                nis: 0,
                educationTax: 0,
                nht: 0,
                totalStatutoryDeductions: 0,
                incomeTaxThreshold: 0,
                taxableIncome: 0,
                incomeTax: 0,
                netIncome: 0
            }
        };
    }

    public render() {
        let salaryBreakdown = !this.state.salaryDataPresent
            ? <p>No salary data present</p>
            : this.renderSalaryDetails(this.state.salaryResponse.details);
        return <div>
            <br />
            <h1>Jamaica Income Tax Calculator 2017/2018</h1>
            <br />
            Gross Salary (Annual): <input type="number" value={this.state.grossSalary} onChange={this.updateGrossSalary} /> &nbsp;
            <button onClick={() => { this.calculateTax() }}>Calculate</button>

            <br /><br />
            { salaryBreakdown }
        </div>;
    }

    updateGrossSalary(event: any) {
        this.setState({ grossSalary: event.target.value });
    }

    calculateTax() {
        fetch('api/Calculate?grossIncome='+this.state.grossSalary)
            .then(response => response.json() as Promise<SalaryInformationResponse>)
            .then(data => {
                this.setState({ salaryResponse: data, salaryDataPresent: true });
            });
    }

    formatMoney(money: number) {
        return `$${Number(money).toFixed(2).replace(/./g, function (c, i, a) {
            return i && c !== "." && ((a.length - i) % 3 === 0) ? `,${c}` : c;
        })}`;
    }

    renderSalaryDetails(details: SalaryDetails) {
        return <div>
            <p>Gross Income: {this.formatMoney(details.grossIncome)}</p>
            <p>Pension: {this.formatMoney(details.pension)}</p>
            <p>NIS: {this.formatMoney(details.nis)}</p>
            <p>Education Tax: {this.formatMoney(details.educationTax)}</p>
            <p>NHT: {this.formatMoney(details.nht)}</p>
            <p>Total Statutory Deductions: {this.formatMoney(details.totalStatutoryDeductions)}</p>
            <p>Income Tax Threshold: {this.formatMoney(details.incomeTaxThreshold)}</p>
            <p>Taxable Income: {this.formatMoney(details.taxableIncome)}</p>
            <p>Income Tax: {this.formatMoney(details.incomeTax)}</p>
            <p>Net Income: {this.formatMoney(details.netIncome)}</p>
            <p>Monthly Salary (Take Home): {this.formatMoney(details.netIncome / 12)}</p>
        </div>;
    }
}