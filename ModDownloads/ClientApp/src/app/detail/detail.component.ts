import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DownloadService } from '../download.service'
import { ModService } from '../mod.service';
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  public name: string;
  private sub: any;
  public DownloadData: Array<any> = [{ data: [], label: '' }];
  public IncreaseData: Array<any> = [{ data: [], label: '' }];

  public totalDownloads: number;
  public dailyDownloads: number;
  public monthlyDownloads: number;
  public yearlyDownloads: number;

  public monthlyDownloadsAllMonths: Array<any> = [];

  constructor(private downloadService: DownloadService, private modService: ModService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.name = params.name;
    });

    this.modService.getByName(this.name).subscribe((data: Array<any>) => {
      let DownloadData2 = [];
        this.downloadService.getById(data).subscribe((data: Array<any>) => {
          let set = { label: this.name, data: [] };
          for (let entry of data) {
            set.data.push({
              x: new Date(entry.timestamp),
              y: entry.downloads
            });
          }

          DownloadData2.push(set);
          this.DownloadData = [...DownloadData2];

        });
   
      this.downloadService.getIncreaseById(data).subscribe((data: { [index: string]: any; }) => {
        let IncreaseData2 = [{ label: this.name, data: [] }];
        for (let [key, value] of Object.entries(data)) {
          IncreaseData2[0].data.push({
            x: new Date(key),
            y: value
          });
          this.IncreaseData = [...IncreaseData2];
        }

        

      });
      this.downloadService.getDailyById(data).subscribe((data: number) => {
        this.dailyDownloads = data;
      });
      this.downloadService.getMonthlyById(data).subscribe((data: number) => {
        this.monthlyDownloads = data;
      });
      this.downloadService.getYearlyById(data).subscribe((data: number) => {
        this.yearlyDownloads = data;
      });
      this.downloadService.getTotalById(data).subscribe((data: number) => {
        this.totalDownloads = data;
      });
      this.downloadService.getAllMonthlyById(data).subscribe((data: { [index: number]: any; }) => {
        let newData = [];
        for (let [key, value] of Object.entries(data)) {
          newData.push({
            month: new Date(2000, +key, 0),
            value: value
          });
          this.monthlyDownloadsAllMonths = [...newData];
        }
     //   console.log(this.monthlyDownloadsAllMonths);
      });
    });

   
 }
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
      }]
    }
  };
  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
