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
    modService.get().subscribe((data: Array<any>) => {
      let DownloadData = [];
      let id = 0;
      for (let entry of data) {
        
        downloadService.getById(entry).subscribe((data: Array<any>) => {
          let set = { label: entry.name, data: [], yAxisID: id};
          console.log(id);
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
 

}
