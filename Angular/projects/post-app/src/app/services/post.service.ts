import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PageResultDto } from "shared-lib";
import { PostDto } from "../entites/post.dto";
import { PORT } from "auth-lib";
import { CreateCommentDto, TreeComment } from "../entites/comment.post.dto";

@Injectable({
    providedIn: 'root'
  })
export class PostService {
    constructor(private http: HttpClient) { }

    public getPosts(
        maxResultCount: number,
        currentPage: number
      ): Observable<PageResultDto<PostDto>> {
        return this.http.get<PageResultDto<PostDto>>(
          `${PORT.postAPI}/Posts/getAll?Page=${currentPage}&Limit=${maxResultCount}`
        );
    }

    public getPostById(id: string): Observable<PostDto> {
      return this.http.get<PostDto>(
        `${PORT.postAPI}/Posts/detail-post/${id}`
      );
    }

    public getAllComment(id: string) : Observable<TreeComment>{
      return this.http.get<TreeComment>(
        `${PORT.postAPI}/Posts/comment-post/${id}`
      )
    }

    public setIdPost(id: string){
      localStorage.setItem('idPost', JSON.stringify(id));
    }

    public createComment(createCommentDto: CreateCommentDto): Observable<any> {
      return this.http.post<any>(
        `${PORT.postAPI}/Comments/create`,
        createCommentDto
      );
    }

    public deleteComment(id: number): Observable<any> {
      return this.http.delete<any>(
        `${PORT.postAPI}/Comments/${id}`
      );
    }
}