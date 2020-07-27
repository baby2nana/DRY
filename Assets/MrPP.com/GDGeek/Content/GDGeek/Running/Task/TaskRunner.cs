using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{

public class TaskRunner : MonoBehaviour, ITaskRunner
{
      private Filter filter_ = new Filter();
		private List<Task> tasks_ = new List<Task>();
		private List<Task> shutdown_ = new List<Task>();

		public static TaskRunner Create(GameObject obj){
			TaskRunner runner = obj.GetComponent<TaskRunner>();
			if(runner == null){
				runner = obj.AddComponent<TaskRunner>();
			}
			return runner;
		}


		public void update(float d){
			
			var tasks = new List<Task>();
			for (int i = 0; i < this.shutdown_.Count; ++i) {
				this.shutdown_ [i].shutDown ();
			}
			this.shutdown_.Clear ();
			for(var i=0; i< this.tasks_.Count; ++i){
				Task task = this.tasks_[i] as Task;
				task.update(d);
				if(!task.isOver()){
					tasks.Add(task);
				}else{
					shutdown_.Add (task);
				}
			}
			this.tasks_ = tasks;
		}

		

		public void addTask(Task task){
			task.init();
			this.tasks_.Add(task);
		}
		
		protected virtual void Update() { 
			float d = filter_.interval(Time.deltaTime);
			this.update (d);
		}
   }
}

