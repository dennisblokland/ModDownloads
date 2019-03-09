import { Component, ViewChild } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts-x';
import { DownloadService } from '../download.service'
import { ModService } from '../mod.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective;

  public totalDownloads: number;
  public dailyDownloads: number;
  public monthlyDownloads: number;
  public yearlyDownloads: number;
  public lineChartData: Array<any> = [{ data: [], label: '' }];
  public mods: object;
  public lineChartType: string = 'line';
  public _modService: any;
  public _downloadService: any;
  public timespans: Array<string> = ["Today", "Past week", "Past month", "Past year", "All Time"];
  public nrSelect = this.timespans[0];
  public lineChartOptions: any = {
    responsive: true,
    elements: {
      point: {
        radius: 0
      }
    },
    scales: {
      xAxes: [{
        type: 'time',
        time: {
          unit: 'day',
          tooltipFormat: 'll HH:MM'
        }
      }],
      yAxes: [{
        id: 0,
        position: 'left',
      }, {
        id: 1,
        position: 'right',

      }]
    },
  };

  constructor(private downloadService: DownloadService, private modService: ModService, private router: Router) {
    this._modService = modService;
    this._downloadService = downloadService;
    var date = new Date()
    date.setHours(0, 0, 0, 0);
    this.getModDataAfterDate(date);
    downloadService.getTotal().subscribe((data: number) => {
      this.totalDownloads = data;
    });
    downloadService.getDaily().subscribe((data: number) => {
      this.dailyDownloads = data;
    });
    downloadService.getMonthly().subscribe((data: number) => {
      this.monthlyDownloads = data;
    });
    downloadService.getYearly().subscribe((data: number) => {
      this.yearlyDownloads = data;
    });
    this.modService.get().subscribe((data: Array<any>) => {
      this.mods = data;
      for (let entry of data) {
        this.downloadService.getTotalById(entry).subscribe((data: number) => {
          entry.downloads = data
        });

      }
    });

  }


  public onChange(event) {
    var date = new Date(), y = date.getFullYear(), m = date.getMonth();
    switch (event.target.value) {
      case "0: Today": {
        date.setHours(0, 0, 0, 0);
        break;
      }
      case "1: Past week": {
        date = this.getMonday(date);
        break;
      }
      case "2: Past month": {
        date = new Date(y, m, 1,0,0,0,0)
        break;
      }
      case "3: Past year": {
        date = new Date(y, 1, 1, 0, 0, 0, 0)
        break;
      }
      case "4: All Time": {
        this.getModDataAllTime();
        return;
      }
      default: {

        break;
      }
    }
    this.getModDataAfterDate(date);
  }

  public getModDataAllTime() {
    this._modService.get().subscribe((data: Array<any>) => {
      let DownloadData = [];
      let id = 0;
      for (let entry of data) {

        this._downloadService.getById(entry).subscribe((data: Array<any>) => {
          let set = { label: entry.name, data: [], yAxisID: id };
          for (let entry of data) {
            set.data.push({
              x: new Date(entry.timestamp),
              y: entry.downloads
            });
          }
          DownloadData.push(set);
          this.lineChartData = [...DownloadData];
          id++;
        });
      }
    });
  }
  public getModDataAfterDate(date) {
    this._modService.get().subscribe((data: Array<any>) => {
      let DownloadData = [];
      let id = 0;
      for (let entry of data) {
        entry.startTime = date;
        this._downloadService.getByIdAfterDate(entry).subscribe((data: Array<any>) => {
          let set = { label: entry.name, data: [], yAxisID: id };
          for (let entry of data) {
            set.data.push({
              x: new Date(entry.timestamp),
              y: entry.downloads
            });
          }
          DownloadData.push(set);
          this.lineChartData = [...DownloadData];
          id++;
        });
      }
    });
  }

  public getMonday(d) {
  d = new Date(d);
  var day = d.getDay(),
      diff = d.getDate() - day + (day == 0 ? -6 : 1); // adjust when day is sunday
    d = new Date(d.setDate(diff));
    var year = d.getUTCFullYear();
    var month = d.getUTCMonth();
    var day = d.getUTCDate();
    return new Date(year, month, day, 0, 0, 0, 0);
}
}
