import React from 'react';
import axios from 'axios';

class Home extends React.Component {
    state = { 
        posts:[]
     }

     componentDidMount = async() => {
       const {data} = await axios.get('api/scraper/getposts');
       this.setState({posts: data})
       console.log(data);
     }

    render() { 
        const readMore = "Read More›";
        return (
            <div>
            {this.state.posts =='' && <h3 style={{textAlign: "center"}}>Loading...</h3>}
            {this.state.posts != '' && 
              <div>
                <h2 style={{textAlign:"center"}}>Newest Posts from The Lakewood Scoop</h2>
                <table className="table table-bordered table-hover table-striped">
                  <thead>
                      <tr>
                          <th>Image</th>
                          <th>Title</th> 
                          <th>Summary</th> 
                          <th>Comments</th>
                      </tr>
                  </thead>
                  <tbody>
                      {this.state.posts.map(p=> 
                            <tr key = {p.id}>
                            <td> <img src = {p.image} height="100" width="100"/> </td>
                            <td><a href={p.url} target="_blank">{p.title} </a></td>
                            <td>{p.summary}<a href={p.url} target = "_blank">{readMore}</a></td>
                            <td>{p.commentCount}</td>
                        </tr>
                      )}
                 </tbody>
                </table>
                </div>
               }
               
            </div>

          );
    }
}
 
export default Home;