using UnityEngine;
using System.Collections;

public interface ISorcererSubscriber {

	void updateMySorcerer(Sorcerer newSorcerer);
	void subscribeToSorcererInstancePublisher();

}
