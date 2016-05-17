import {Component} from 'angular2/core';
import {Injectable} from 'angular2/core';
import { Http, Response } from 'angular2/http';

import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'MusicSchedulerApp',
    template: '<p>{{title}}</p>'
})
export class App {
    title = "hallo";

    constructor(private _http: Http) {
        this._http.get('api/info')
            .map(this.handleInfo)
            .catch(this.handleError);
    }

    private handleInfo(res: Response) {
        if (res.status < 200 || res.status >= 300) {
            throw new Error('Response status: ' + res.status);
        }
        let body = res.json();

        alert("test2");
        return body.data || {};
    }

    private handleError(error: any) {
        alert("test");
        console.log(error);
        return Observable.throw(error.message);
    }
}