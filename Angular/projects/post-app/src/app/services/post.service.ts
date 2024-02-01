import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PageResultDto } from "shared-lib";
import { PostDto } from "../entites/post.dto";
import { PORT } from "auth-lib";

@Injectable({
    providedIn: 'root'
  })
export class PostService {
    constructor(private http: HttpClient) { }

    public getProducts(
        maxResultCount: number,
        currentPage: number
      ): Observable<PageResultDto<PostDto>> {
        return this.http.get<PageResultDto<PostDto>>(
          `${PORT.postAPI}/Posts/getAll?Page=${currentPage}&Limit=${maxResultCount}`
        );
      }
}